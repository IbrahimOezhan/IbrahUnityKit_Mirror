#region

using System.Collections.Generic;
using System.Linq;
using IbrahKit.Core;
using IbrahKit.Debugging;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

#endregion

namespace IbrahKit.Localization
{
    /// <summary>
    ///     Holds the localization data
    /// </summary>
    public class Local_Manager_Data : SerializedScriptableObject, IFileWatcher, ISelfValidator
    {
        [SerializeField, SerializeReference] private ILocalDataParser localDataParser;

        [SerializeField, OnValueChanged(nameof(OnFileUpdate))]
        private List<Local_Language> languages;

        [OdinSerialize, ReadOnly] private Dictionary<string, string[]> keyValuePairs = new();
        [OdinSerialize, ReadOnly] private Dictionary<SystemLanguage, Local_Language> languageDict = new();
        [OdinSerialize, ReadOnly] private Dictionary<Local_Language, int> languageIndexDict = new();

        public List<Local_Language> Languages => languages;
        public Dictionary<Local_Language, int> LanguageIndexDict => languageIndexDict;
        public Dictionary<SystemLanguage, Local_Language> LanguageDict => languageDict;

        [Button]
        public void OnFileUpdate()
        {
            SelfValidationResult result = new SelfValidationResult();

            Validate(result);

            if (result[0].ResultType == SelfValidationResult.ResultType.Error)
            {
                return;
            }

            keyValuePairs.Clear();

            languageIndexDict.Clear();

            for (var i = 0; i < languages.Count; i++)
            {
                localDataParser.Parse(languages[i].GetFile().text, keyValuePairs, i);

                languageIndexDict[languages[i]] = i;
            }

            languageDict = languages.ToDictionary(x => x.GetSys(), (x) => x);

            Local_Key_Table.Instance.Values = keyValuePairs.Select(kvp => kvp.Key).ToList();
        }

        public void Validate(SelfValidationResult result)
        {
            for (var i = 0; i < languages.Count; i++)
            {
                if (languages[i].GetFile() == null)
                {
                    result.AddError("Language needs file");
                }
            }
        }

        public bool TryGetString(string key, Local_Language language, out string result)
        {
            result = string.Empty;

            int index = languageIndexDict[language];

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

            IbrahDebug.Log($"String with key{key} and language index {index} and language {language} empty");

            return false;
        }

        public Local_Language GetFirstLanguage() => languages.First();
    }
}