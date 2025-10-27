using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

namespace IbrahKit
{
    [DefaultExecutionOrder(Execution_Order.local)]
    public partial class Local_Manager : Manager_DDOL<Local_Manager>
    {
        public const string DROP = "Localization";

        public const string SYS = "SysLanguage";

        private const string SETTING = "language";

        private const string SAVE = "LocalizationManager";

        private int currentIndex;

        private LocalLanguage current;

        private SaveData saveData;

        private List<Local_Processor> processors = new();

        [SerializeField] private Local_Config config;

        [HideInInspector] public Action OnLanguageChanged;

        protected override void OnAwake()
        {
            base.OnAwake();

            saveData = (SaveData)Save_Manager.GetInstance().Load(SAVE, new SaveData());

            SetLanguage(GetSystemLanguage(!saveData.SetAttempt() ? Application.systemLanguage : saveData.GetLanguage()));

            AddProcessor(new Local_BreakProcessor());

            Debug.Log($"{nameof(Local_Manager)} initialized successfully", Color.green);
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                current.IsValid(out SystemLanguage sys);

                saveData.SetLanguage(sys);

                Save_Manager.GetInstance().Return(SAVE, saveData);
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

        public void UpdateLanguage()
        {
            OnLanguageChanged?.Invoke();
        }

        public void Set(int index)
        {
            if (index < 0 || index >= config.GetLanguages().Count)
            {
                Debug.LogWarning($"Index with value {index} out of range for range 0-{config.GetLanguages().Count - 1}");
                return;
            }

            SetLanguage(config.GetLanguages()[index]);
        }

        public void SetNext(int dir)
        {
            SetLanguage(GetNext(dir));
        }

        public void SetLanguage(LocalLanguage lang)
        {
            current = lang;

            currentIndex = config.GetLanguages().IndexOf(lang);

            UpdateLanguage();
        }

        private LocalLanguage GetSystemLanguage(SystemLanguage systemLanguage)
        {
            LocalLanguage found = config.GetLanguages().Find(x => x.GetSystemLanguage() == systemLanguage);

            if (found == null)
            {
                return current;
            }
            else return found;
        }

        private LocalLanguage GetNext(int dir)
        {
            int newIndex = Number_Utilities.LoopNumber(currentIndex + dir, 0, config.GetLanguages().Count - 1);
            return config.GetLanguages()[newIndex];
        }

        public LocalLanguage GetCurrent()
        {
            return current;
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

                if (!GetString(key, config.GetLanguages()[0], out result))
                {
                    Debug.LogWarning($"Localzation for key {key} does not exist in default language {config.GetLanguages()[0]}");
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
            return config.GetLanguages().IndexOf(language);
        }

        public int LanguageCount()
        {
            return config.GetLanguages().Count;
        }
        private bool GetString(string key, LocalLanguage language, out string result)
        {
            result = "";

            if (config.TryGetValue(key, out var value))
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
    }
}