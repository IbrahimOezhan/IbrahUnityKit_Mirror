#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.Core;
using IbrahKit.Debugging;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;

#endregion

namespace IbrahKit.Localization
{
    /// <summary>
    ///     Holds the localization data
    /// </summary>
    public class Local_Manager_Data : SerializedScriptableObject, IFileWatcher, ISelfValidator
    {
        [OdinSerialize, OnValueChanged(nameof(OnParserChanged)), InlineProperty]
        private ILocalDataParser localDataParser;

        [SerializeField, OnValueChanged(nameof(OnFileUpdate))]
        private List<Local_Language> languages = new();

        [OdinSerialize, ReadOnly] private Dictionary<string, string[]> keyValuePairs = new();
        [OdinSerialize, ReadOnly] private Dictionary<SystemLanguage, Local_Language> languageDict = new();
        [OdinSerialize, ReadOnly] private Dictionary<Local_Language, int> languageIndexDict = new();

        public List<Local_Language> Languages => languages;
        public Dictionary<Local_Language, int> LanguageIndexDict => languageIndexDict;
        public Dictionary<SystemLanguage, Local_Language> LanguageDict => languageDict;

#if UNITY_EDITOR

        public static Local_Manager_Data Instance
        {
            get
            {
                string[] guids = AssetDatabase.FindAssets($"t:{nameof(Local_Manager_Data)}");

                switch (guids.Length)
                {
                    case 1:
                        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                        return AssetDatabase.LoadAssetAtPath<Local_Manager_Data>(path);
                    case 0:
                        IbrahDebug.LogWarning("No DB found");
                        throw new Exception("No DB found");
                    default:
                        IbrahDebug.LogWarning("More than 1 DB found");
                        throw new Exception("More than 1 DB found");
                }
            }
        }

#endif

        [Button]
        public void OnFileUpdate()
        {
            if (languages.Any(x => x.GetFile() == null))
            {
                return;
            }

            keyValuePairs.Clear();

            languageIndexDict.Clear();

            for (var i = 0; i < languages.Count; i++)
            {
                localDataParser.Parse(languages[i].GetFile().text, keyValuePairs, i, languages.Count);

                languageIndexDict[languages[i]] = i;
            }

            languageDict = languages.ToDictionary(x => x.GetSys(), (x) => x);

#if UNITY_EDITOR
            Local_Key_Table.Instance.Values = keyValuePairs.Select(kvp => kvp.Key).ToList();
#endif
        }

        public void Validate(SelfValidationResult result)
        {
            foreach (var t in languages)
            {
                if (t.GetFile() == null)
                {
                    result.AddError("Language needs file");
                }
            }
        }

        private void OnParserChanged()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        public bool TryGetString(string key, Local_Language language, out string result)
        {
            return TryGetString(key,LanguageIndexDict[language] , out result);
        }

        public bool TryGetString(string key, int index, out string result)
        {
            result = string.Empty;

            if (!keyValuePairs.TryGetValue(key, out string[] localizedValues))
            {
                IbrahDebug.Log($"String with key{key} doesnt exist");

                return false;
            }

            if (!(index >= 0 && index < localizedValues.Length))
            {
                IbrahDebug.LogWarning($"Index {index} out of range for length {localizedValues.Length} and key {key}");

                return false;
            }

            result = localizedValues[index];

            if (!result.IsEmpty()) return true;

            IbrahDebug.Log($"String with key{key} and language index {index} and language {index} empty");

            return false;
        }

        public Local_Language GetFirstLanguage() => languages.First();
    }
}