using IbrahKit.Save;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

namespace IbrahKit.Localization
{
    [DefaultExecutionOrder(Execution_Order.local)]
    public partial class Local_Manager : Manager_DDOL<Local_Manager>
    {
        public const string DROP = "Localization";

        public const string SYS = "SysLanguage";

        private const string SAVE = "LocalizationManager";

        private int currentIndex;

        private Local_Language current;

        private SaveData saveData;

        private readonly List<Local_Processor> processors = new();

        [SerializeField] private Local_Config config;

        [HideInInspector] public Action OnLanguageChanged;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            if (Save_Manager.GetInstance().TryLoad(SAVE, new SaveData(), out saveData))
            {
                saveData = (SaveData)Save_Manager.GetInstance().Load(SAVE, new SaveData());

                SetLanguage(GetSystemLanguage(!saveData.SetAttempt() ? Application.systemLanguage : saveData.GetLanguage()));

                AddProcessor(new Local_BreakProcessor());
            }
        }

        protected override void InstanceDestroy()
        {
            current.IsValid(out SystemLanguage sys);

            saveData.SetLanguage(sys);

            if (Save_Manager.TryGet(out Save_Manager result))
            {
                result.Return(SAVE, saveData);
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
                IbrahDebug.LogWarning($"Index with value {index} out of range for range 0-{config.GetLanguages().Count - 1}");
                return;
            }

            SetLanguage(config.GetLanguages()[index]);
        }

        public void SetNextLanguage(int dir)
        {
            SetLanguage(GetNext(dir));
        }

        public void SetLanguage(Local_Language lang)
        {
            current = lang;

            currentIndex = config.GetLanguages().IndexOf(lang);

            UpdateLanguage();
        }

        private Local_Language GetSystemLanguage(SystemLanguage systemLanguage)
        {
            Local_Language found = config.GetLanguages().Find(x => x.GetSystemLanguage() == systemLanguage);

            if (found == null)
            {
                return current;
            }
            else return found;
        }

        private Local_Language GetNext(int dir)
        {
            int newIndex = Math_Utilities.LoopNumber(currentIndex + dir, 0, config.GetLanguages().Count - 1);

            return config.GetLanguages()[newIndex];
        }

        public Local_Language GetCurrent()
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
            if (!config.TryGetString(key, current, out string result))
            {
                IbrahDebug.LogWarning($"Localzation for key {key} does not exist in select language {current}");

                if (!config.TryGetString(key, config.GetLanguages()[0], out result))
                {
                    IbrahDebug.LogWarning($"Localzation for key {key} does not exist in default language {config.GetLanguages()[0]}");
                }
            }

            for (int i = 0; i < processors.Count; i++)
            {
                result = processors[i].Process(result);
            }

            return String_Utilities.IsEmpty(result) ? $"Error {key}" : result.SafeFormat(parameters);
        }

        public int IndexOf(Local_Language language)
        {
            return config.GetLanguages().IndexOf(language);
        }

        public int LanguageCount()
        {
            return config.GetLanguages().Count;
        }

        [Serializable]
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