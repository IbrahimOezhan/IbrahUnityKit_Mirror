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

        private Local_Language currentLanguage;

        public Action onLanguageChanged;

        private SaveData saveData;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            saveData = SimpleSaveManager.GetInstance().GetSave().Get(new SaveData());

            Local_Language language = GetManagerData()
                .LanguageDict[!saveData.SetAttempt() ? Application.systemLanguage : saveData.GetLanguage()];

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
            if (index < 0 || index >= GetManagerData().Languages.Count)
            {
                IbrahDebug.LogWarning(
                    $"Index with value {index} out of range for range 0-{GetManagerData().Languages.Count - 1}");

                return;
            }

            SetLanguage(GetManagerData().Languages[index]);
        }

        public void SetLanguage(Local_Language lang)
        {
            currentLanguage = lang;

            saveData.SetLanguage(lang.GetSys());

            UpdateLanguage();
        }

        public string GetString(string key, string fallback = null, params object[] parameters)
        {
            string s = GetString(key, parameters);

            if (s == $"Error {key}") s = fallback;

            return s;
        }

        public string GetString(string key, params object[] parameters)
        {
            if (GetManagerData().TryGetString(key, currentLanguage, out string result))
            {
                return ProcessText(result.SafeFormat(parameters));
            }

            IbrahDebug.LogWarning($"Localization for key {key} does not exist in select language {currentLanguage}");

            if (GetManagerData().TryGetString(key, GetManagerData().Languages.First(), out result))
            {
                return ProcessText(result.SafeFormat(parameters));
            }

            IbrahDebug.LogWarning(
                $"Localization for key {key} does not exist in default language {GetManagerData().Languages.First()}");

            return $"Error: {key}";
        }

        private Local_Language GetNext(int dir)
        {
            int newIndex =
                (GetManagerData().LanguageIndexDict[currentLanguage] + dir).Loop(0,
                    GetManagerData().Languages.Count - 1);

            return GetManagerData().Languages[newIndex];
        }
    }
}