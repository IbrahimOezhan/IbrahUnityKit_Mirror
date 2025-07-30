using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using UnityEngine;

namespace IbrahKit
{
    [DefaultExecutionOrder(Execution_Order.local)]
    public class Local_Manager : SerializedMonoBehaviour
    {
        public const string DROP = "Localization";

        public const string SYS = "SysLanguage";

        private const string LANG = "Language";

        private const string SETTING = "language";

        private const string SAVE = "LocalizationManager";

        private int currentIndex;

        private LocalLanguage current;

        private SaveData saveData;

        private List<Local_Processor> processors = new();

        [SerializeField] private TextAsset localizationAssets;

        [ShowInInspector, OdinSerialize] private Dictionary<string, string[]> keyValuePairs = new();

        [SerializeField, OdinSerialize] private List<LocalLanguage> languages = new();

        [HideInInspector] public Action OnLanguageChanged;

        public static Local_Manager Instance;

        public static bool Exists(out Local_Manager manager, bool throwWarning = true)
        {
            manager = Instance;

            bool exists = Instance != null && Instance.gameObject != null;

            if (throwWarning && !exists)
            {
                Debug.LogWarning($"{nameof(Local_Manager)} does not exist");
            }

            return exists;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            transform.parent = null;

            DontDestroyOnLoad(gameObject);

            saveData = (SaveData)Save_Manager.Instance.Load(SAVE, new SaveData());

            Init();

            if (!saveData.SetAttempt())
            {
                SetLanguage(GetSystemLanguage(Application.systemLanguage));
            }
            else
            {
                SetLanguage(GetSystemLanguage(saveData.GetLanguage()));
            }

            AddProcessor(new Local_BreakProcessor());

            Debug.Log($"{nameof(Local_Manager)} initialized successfully", Color.green);
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                current.IsValid(out SystemLanguage sys);

                saveData.SetLanguage(sys);

                Save_Manager.Instance.Return(SAVE, saveData);
            }
        }

        public void AddProcessor(Local_Processor processor)
        {
            processors.Add(processor);
        }

        public void RemoveProcessor(Local_Processor processor)
        {
            processors.Remove(processor);
        }

        [Button]
        private void Init()
        {
            char seperator = ';';

            languages = new();

            keyValuePairs = new();

            List<string> lines = localizationAssets.text.Split("\n").ToList();

            lines.RemoveAll(x => String_Utilities.IsEmpty(x.Trim().Replace(seperator.ToString(), "")));

            if (lines.Count == 0)
            {
                Debug.LogWarning("No elements after trimming");
                return;
            }

            string[] rowOne = GetRow(lines[0], seperator);

            JsonSerializerOptions options = new()
            {
                IncludeFields = true
            };

            for (int i = 1; i < rowOne.Length; i++)
            {
                if (!Parse_Utilties.IsValidJson(rowOne[i]))
                {
                    Debug.LogWarning($"Invalid json in row 0 column {i}");
                    return;
                }

                try
                {
                    LocalLanguage ll = JsonSerializer.Deserialize<LocalLanguage>(rowOne[i], options);

                    if (!ll.IsValid(out _))
                    {
                        Debug.LogWarning($"System language in column {i} cannot be parsed");
                        return;
                    }

                    languages.Add(ll);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    return;
                }
            }

            for (int i = 1; i < lines.Count; i++)
            {
                List<string> row = GetRow(lines[i], seperator).ToList();

                int requiredCount = languages.Count + 1;

                while (row.Count < requiredCount)
                {
                    row.Add(string.Empty);
                }

                while (row.Count > requiredCount)
                {
                    Debug.LogWarning($"Removed {row[row.Count - 1]} from the back");
                    row.RemoveAt(row.Count - 1);
                }

                string key = row[0];

                row.RemoveAt(0);

                keyValuePairs.TryAdd(key, row.ToArray());
            }

            Dropdown_Utilities.CreateDropdown(keyValuePairs.Keys.ToList(), DROP);

            Dropdown_Utilities.CreateDropdown(languages.Select(x => x.GetNative()).ToList(), LANG);

            Dropdown_Utilities.CreateDropdown(languages.Select(x => x.GetSys()).ToList(), SYS);
        }

        public void UpdateLanguage()
        {
            OnLanguageChanged?.Invoke();
        }

        public void Set(int index)
        {
            if (index < 0 || index >= languages.Count)
            {
                Debug.LogWarning($"Index with value {index} out of range for range 0-{languages.Count - 1}");
                return;
            }

            SetLanguage(languages[index]);
        }

        public void SetNext(int dir)
        {
            SetLanguage(GetNext(dir));
        }

        public void SetLanguage(LocalLanguage lang)
        {
            current = lang;

            currentIndex = languages.IndexOf(lang);

            UpdateLanguage();
        }

        private LocalLanguage GetSystemLanguage(SystemLanguage systemLanguage)
        {
            LocalLanguage found = languages.Find(x => x.GetSystemLanguage() == systemLanguage);

            if (found == null)
            {
                return current;
            }
            else return found;
        }

        private LocalLanguage GetNext(int dir)
        {
            int newIndex = Number_Utilities.LoopNumber(currentIndex + dir, 0, languages.Count - 1);
            return languages[newIndex];
        }

        public LocalLanguage GetCurrent()
        {
            return current;
        }

        private string[] GetRow(string line, char seperator)
        {
            return line.Split(seperator);
        }

        public string GetString(string key, string fallback, params string[] parameters)
        {
            string s = GetString(key, parameters);

            if (s == $"Error {key}") s = fallback;

            return s;
        }

        public string GetString(string key, params string[] parameters)
        {
            string result = "";

            if (!GetString(key, current, out result))
            {
                Debug.LogWarning($"Localzation for key {key} does not exist in select language {current}");

                if (!GetString(key, languages[0], out result))
                {
                    Debug.LogWarning($"Localzation for key {key} does not exist in default language {languages[0]}");
                }
            }

            for (int i = 0; i < processors.Count; i++)
            {
                result = processors[i].Process(result);
            }

            return String_Utilities.IsEmpty(result) ? $"Error {key}" : FormatString(result, parameters);
        }

        private string FormatString(string text, params string[] parameters)
        {
            if (parameters == null || parameters.Length == 0) return text;

            try
            {
                return String.Format(text, parameters);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return text;
            }
        }

        public int IndexOf(LocalLanguage language)
        {
            return languages.IndexOf(language);
        }

        public int LanguageCount()
        {
            return languages.Count;
        }
        private bool GetString(string key, LocalLanguage language, out string result)
        {
            result = "";

            if (keyValuePairs.TryGetValue(key, out var value))
            {
                result = value[IndexOf(language)];
            }

            bool empty = String_Utilities.IsEmpty(result);

            if (empty)
            {
                Debug.Log($"String with key{key} and language index {currentIndex} and language {language} empty");
            }

            return !empty;
        }

        [System.Serializable]
        private class SaveData : Savable
        {
            [JsonInclude]
            private bool attemptedGetSys;
            [JsonInclude]
            private SystemLanguage currentLanguage;

            public bool SetAttempt()
            {
                bool previous = attemptedGetSys;
                attemptedGetSys = true;
                return previous;
            }

            public SystemLanguage GetLanguage()
            {
                return currentLanguage;
            }

            public void SetLanguage(SystemLanguage language)
            {
                currentLanguage = language;
            }
        }

        [System.Serializable]
        public class LocalLanguage
        {
            [JsonInclude, SerializeField]
            private string sysLang;

            [JsonInclude, SerializeField]
            private string nativeLocal;

            [SerializeField] private bool skip;

            public bool IsValid(out SystemLanguage result)
            {
                return Enum.TryParse(sysLang, out result);
            }

            public SystemLanguage GetSystemLanguage()
            {
                return Enum.Parse<SystemLanguage>(sysLang);
            }

            public string GetSys()
            {
                return sysLang;
            }

            public string GetNative()
            {
                return nativeLocal;
            }

            public bool GetSkip()
            {
                return skip;
            }

            public override string ToString()
            {
                return sysLang;
            }
        }
    }
}