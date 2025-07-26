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

        [ShowInInspector, OdinSerialize] private Dictionary<string, string[]> keyValuePairs = new();

        [SerializeField] private TextAsset localizationAssets;

        [SerializeField, OdinSerialize] private LinkedList<LocalLanguage> languages;

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

            Debug.Log($"{nameof(Local_Manager)} initialized successfully", Color.green);

            InitManager();

            if (!saveData.SetAttempt()) SetLanguage(GetSystemLanguage(Application.systemLanguage));
            else SetLanguage(GetSystemLanguage(saveData.GetLanguage()));
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

        [Button]
        private void InitManager()
        {
            languages = new();
            keyValuePairs = new();

            List<string> lines = localizationAssets.text.Split("\n").ToList();

            lines.RemoveAll(x => String_Utilities.IsEmpty(x.Trim().Replace(";", "")));

            if (lines.Count == 0)
            {
                Debug.LogWarning("No elements after trimming");
                return;
            }

            int lastAmount = SeperatorAmount(lines[0], ';');

            for (int i = 0; i < lines.Count; i++)
            {
                if (!ValidFormatting(lines[i], lastAmount, ';'))
                {
                    Debug.LogWarning("Uneven amount of columns " + i);
                    return;
                }
            }

            string firstLine = lines[0];

            string[] firstRow = GetRow(firstLine, ';');

            if (firstRow.Length == 1)
            {
                Debug.LogWarning("Not enough columns. At least one key column and one value column required");
                return;
            }

            JsonSerializerOptions options = new()
            {
                IncludeFields = true
            };

            for (int i = 1; i < firstRow.Length; i++)
            {
                LocalLanguage ll = JsonSerializer.Deserialize<LocalLanguage>(firstRow[i], options);

                if (!ll.IsValid(out _))
                {
                    Debug.LogWarning($"System language in column {i} cannot be parsed");
                    return;
                }

                languages.AddLast(ll);
            }

            for (int i = 1; i < lines.Count; i++)
            {
                List<string> row = GetRow(lines[i], ';').ToList();
                string key = row[0];
                row.RemoveAt(0);
                keyValuePairs.TryAdd(key, row.ToArray());
            }

            Debug.Log(keyValuePairs.Keys.ToList().Count);

            String_Utilities.CreateDropdown(keyValuePairs.Keys.ToList(), DROP);

            String_Utilities.CreateDropdown(languages.Select(x => x.GetNative()).ToList(), LANG);

            String_Utilities.CreateDropdown(languages.Select(x => x.GetSys()).ToList(), SYS);
        }

        public void UpdateLanguage()
        {
            OnLanguageChanged?.Invoke();
        }

        public void SetNext()
        {
            SetLanguage(GetNext());
        }

        public void SetLanguage(LocalLanguage lang)
        {
            current = lang;

            LinkedListNode<LocalLanguage> curr = languages.First;

            int index = 0;

            while (curr != null)
            {
                if (curr.Value == lang)
                {
                    currentIndex = index;
                    break;
                }
                curr = curr.Next;
                index++;
            }

            UpdateLanguage();
        }

        private LocalLanguage GetSystemLanguage(SystemLanguage systemLanguage)
        {
            LinkedListNode<LocalLanguage> curr = languages.First;

            while (curr != null)
            {
                if (curr.Value.IsValid(out SystemLanguage sys) && sys == systemLanguage)
                {
                    return curr.Value;
                }
                curr = curr.Next;
            }

            return languages.First.Value;
        }

        private LocalLanguage GetNext()
        {
            LinkedListNode<LocalLanguage> first = languages.Find(current);
            LinkedListNode<LocalLanguage> curr = first;

            while (curr.Value.GetSkip())
            {
                curr = curr.Next;

                if (curr == null)
                {
                    curr = languages.First;
                }

                if (curr == first)
                {
                    Debug.LogWarning("No usable language found");
                    return current;
                }
            }

            return curr.Value;
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

                if (!GetString(key, languages.First.Value, out result))
                {
                    Debug.LogWarning($"Localzation for key {key} does not exist in default language {languages.First.Value}");
                }
            }

            return String_Utilities.IsEmpty(result) ? $"Error {key}" : FormatString(result);
        }

        private string FormatString(string text, params string[] parameters)
        {
            try
            {
                return String.Format(text, parameters);
            }
            catch
            {
                Debug.LogWarning("Localized text expects params but there are not enough or none at all");
                return text;
            }
        }

        private bool GetString(string key, LocalLanguage language, out string result)
        {
            result = "";

            if (keyValuePairs.TryGetValue(key, out var value))
            {
                result = value[currentIndex];
            }

            return !String_Utilities.IsEmpty(result);
        }

        private int SeperatorAmount(string text, char separator)
        {
            int amount = 0;
            foreach (var item in text)
            {
                if (item == separator)
                    amount++;
            }
            return amount;
        }

        public LocalLanguage GetCurrent()
        {
            return current;
        }

        public int CurrentIndex()
        {
            return currentIndex;
        }

        private bool ValidFormatting(string text, int baseAmount, char seperator)
        {
            return (SeperatorAmount(text, seperator) == baseAmount);
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

