using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static IbrahKit.Localization.Local_Manager;

namespace IbrahKit.Localization
{
    [CreateAssetMenu(fileName = "LocalConfig", menuName = "ScriptableObjects/LocalConfig")]
    public class Local_Config : SerializedScriptableObject, IFileWatcher
    {
        private const string LANG = "Language";

        [SerializeField, OnValueChanged(nameof(Update))] private TextAsset localizationAssets;

        [ShowInInspector, OdinSerialize] private Dictionary<string, string[]> keyValuePairs = new();

        [SerializeField, OdinSerialize, NonSerialized] private List<Local_Language> languages = new();

        [SerializeField] private char seperator;

        public Local_Language GetFirstLanguage() => languages[0];

        public List<Local_Language> GetLanguages() => languages;

        public bool TryGetString(string key, Local_Language language, out string result)
        {
            result = "";

            int index = languages.IndexOf(language);

            if (keyValuePairs.TryGetValue(key, out var value))
            {
                if (index >= 0 && index < value.Length)
                {
                    result = value[index];
                }
                else
                {
                    IbrahDebug.LogWarning($"Index {index} out of range for length {value.Length} and key {key}");
                    return false;
                }
            }

            bool empty = String_Utilities.IsEmpty(result);

            if (empty)
            {
                IbrahDebug.Log($"String with key{key} and language index {index} and language {language} empty");
            }

            return !empty;
        }

        [Button]
        public void Update()
        {
            keyValuePairs.Clear();

            List<string> lines = localizationAssets.text.Split("\n").Where(x => String_Utilities.IsEmpty(x.Trim().Replace(seperator.ToString(), ""))).ToList();

            if (lines.Count == 0)
            {
                IbrahDebug.LogWarning("No elements after trimming");
                return;
            }

            if(!TryGetLanguages(out languages, lines[0].Split(seperator)))
            {
                return;
            }

            PopulateDictionary(lines, seperator);

            Key_Database_Finder.TrySetKeys(DROP, keyValuePairs.Keys.OrderBy(x => x).ToList());

            Key_Database_Finder.TrySetKeys(LANG, languages.Select(x => x.GetNative()).ToList());

            Key_Database_Finder.TrySetKeys(SYS, languages.Select(x => x.GetSys()).ToList());
        }

        private bool TryGetLanguages(out List<Local_Language> languages, string[] line)
        {
            languages = new();

            for (int i = 1; i < line.Length; i++)
            {
                if (!Parse_Utilities.IsValidJson(line[i]))
                {
                    IbrahDebug.LogWarning($"Invalid json in row 0 column {i}");
                    return false;
                }

                try
                {
                    if (!Json_Utilities.TryDeserialize(line[i], out Local_Language result))
                    {
                        return false;
                    }

                    if (!result.IsValid(out _))
                    {
                        IbrahDebug.LogWarning($"System language in column {i} cannot be parsed");
                        return false;
                    }

                    languages.Add(result);
                }
                catch (Exception e)
                {
                    IbrahDebug.LogException(e);
                    return false;
                }
            }

            return true;
        }

        private void PopulateDictionary(List<string> lines, char seperator)
        {
            for (int i = 1; i < lines.Count; i++)
            {
                List<string> row = lines[i].Split(seperator).ToList();

                int requiredCount = languages.Count + 1;

                while (row.Count < requiredCount)
                {
                    row.Add(string.Empty);
                }

                while (row.Count > requiredCount)
                {
                    IbrahDebug.LogWarning($"Removed {row[^1]} from the back");
                    row.RemoveAt(row.Count - 1);
                }

                string key = row[0];

                row.RemoveAt(0);

                keyValuePairs.TryAdd(key, row.ToArray());
            }
        }
    }
}