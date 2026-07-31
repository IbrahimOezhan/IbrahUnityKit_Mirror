#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.Core;
using IbrahKit.Debugging;
using IbrahKit.Manager;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.Localization
{
    /// <summary>
    ///     Communicates with various classes to provide the correct localization
    /// </summary>
    [DefaultExecutionOrder(Execution_Order.local)]
    public partial class Local_Manager : Manager_Global<Local_Manager, Local_Manager_Data>
    {
        private readonly List<Local_Processor> processors = new();

        private int currentIndex;

        private Local_Language currentLanguage;

        public Action onLanguageChanged;

        private SaveData saveData;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            saveData = SimpleSaveManager.GetInstance().GetSave().Get(new SaveData());

            Local_Language language =
                GetSystemLanguage(!saveData.SetAttempt() ? Application.systemLanguage : saveData.GetLanguage());

            if (language != null) SetLanguage(language);

            AddProcessor(new Local_BreakProcessor());
        }

        public void AddProcessor(Local_Processor processor)
        {
            if (processor == null)
            {
                IbrahDebug.LogError("Processor is null");
                return;
            }

            processors.Add(processor);
        }

        public void RemoveProcessor(Local_Processor processor)
        {
            if (processor == null)
            {
                IbrahDebug.LogError("Processor is null");
                return;
            }

            processors.Remove(processor);
        }

        private string ProcessText(string text)
        {
            processors.ForEach(x => x.Process(text));

            return text;
        }

        public void UpdateLanguage()
        {
            onLanguageChanged?.Invoke();
        }

        public void NextLanguage(int dir)
        {
            SetLanguage(GetNext(dir));
        }

        public void SetLanguage(int index)
        {
            if (index < 0 || index >= GetManagerData().GetLanguages().Count)
            {
                IbrahDebug.LogWarning(
                    $"Index with value {index} out of range for range 0-{GetManagerData().GetLanguages().Count - 1}");

                return;
            }

            SetLanguage(GetManagerData().GetLanguages().ElementAt(index).Value);
        }

        public void SetLanguage(Local_Language lang)
        {
            currentLanguage = lang;

            if (currentLanguage.IsValid(out SystemLanguage sys))
            {
                saveData.SetLanguage(sys);
            }

            currentIndex = GetManagerData().GetLanguages().IndexOfKey(lang.GetSystemLanguage());

            UpdateLanguage();
        }

        public string GetString(string key, string fallback, params string[] parameters)
        {
            string s = GetString(key, parameters);

            if (s == $"Error {key}") s = fallback;

            return s;
        }

        public string GetString(string key, params string[] parameters)
        {
            if (GetManagerData().TryGetString(key, currentLanguage, out string result))
            {
                return ProcessText(result.SafeFormat(parameters));
            }

            IbrahDebug.LogWarning($"Localization for key {key} does not exist in select language {currentLanguage}");

            if (GetManagerData().TryGetString(key, GetManagerData().GetLanguages().First().Value, out result))
            {
                return ProcessText(result.SafeFormat(parameters));
            }

            IbrahDebug.LogWarning(
                $"Localization for key {key} does not exist in default language {GetManagerData().GetLanguages().First()}");

            return $"Error: {key}";
        }

        private Local_Language GetSystemLanguage(SystemLanguage systemLanguage)
        {
            return GetManagerData().GetLanguages().GetValueOrDefault(systemLanguage, currentLanguage);
        }

        private Local_Language GetNext(int dir)
        {
            int newIndex = (currentIndex + dir).Loop(0, GetManagerData().GetLanguages().Count - 1);

            return GetManagerData().GetLanguages().ElementAt(newIndex).Value;
        }

        public Local_Language GetCurrent() => currentLanguage;

        public int IndexOf(Local_Language language) =>
            GetManagerData().GetLanguages().IndexOfKey(language.GetSystemLanguage());

        public int LanguageCount() => GetManagerData().GetLanguages().Count;
    }
}