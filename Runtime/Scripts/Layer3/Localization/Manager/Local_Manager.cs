#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.Core;
using IbrahKit.Debugging;
using IbrahKit.Save;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.Localization
{
    [DefaultExecutionOrder(Execution_Order.local)]
    public partial class Local_Manager : Manager_Global<Local_Manager, Local_Manager_Data>
    {
        public const string DROP = "Localization";

        public const string SYS = "SysLanguage";

        private const string SAVE = "LocalizationManager";

        private int currentIndex;

        private Local_Language currentLanguage;

        private SaveData saveData;

        private readonly List<Local_Processor> processors = new();

        [HideInInspector] public Action OnLanguageChanged;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            if (Save_Manager.GetInstance().TryLoad(SAVE, out saveData))
            {
                Local_Language language =
                    GetSystemLanguage(!saveData.SetAttempt() ? Application.systemLanguage : saveData.GetLanguage());

                if (language != null) SetLanguage(language);

                AddProcessor(new Local_BreakProcessor());
            }
        }

        protected override void InstanceDestroy()
        {
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

        private string ProcessText(string text)
        {
            processors.ForEach(x => x.Process(text));

            return text;
        }

        public void UpdateLanguage()
        {
            OnLanguageChanged?.Invoke();
        }

        public void Set(int index)
        {
            if (index < 0 || index >= GetManagerData().GetLanguages().Count)
            {
                IbrahDebug.LogWarning(
                    $"Index with value {index} out of range for range 0-{GetManagerData().GetLanguages().Count - 1}");

                return;
            }

            SetLanguage(GetManagerData().GetLanguages()[index]);
        }

        public void SetNextLanguage(int dir)
        {
            SetLanguage(GetNext(dir));
        }

        public void SetLanguage(Local_Language lang)
        {
            currentLanguage = lang;

            if (currentLanguage.IsValid(out SystemLanguage sys))
            {
                saveData.SetLanguage(sys);
            }

            currentIndex = GetManagerData().GetLanguages().IndexOf(lang);

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

            IbrahDebug.LogWarning($"Localzation for key {key} does not exist in select language {currentLanguage}");

            if (GetManagerData().TryGetString(key, GetManagerData().GetLanguages().First(), out result))
            {
                return ProcessText(result.SafeFormat(parameters));
            }

            IbrahDebug.LogWarning(
                $"Localzation for key {key} does not exist in default language {GetManagerData().GetLanguages().First()}");

            return $"Error: {key}";
        }

        private Local_Language GetSystemLanguage(SystemLanguage systemLanguage)
        {
            Local_Language found = GetManagerData().GetLanguages().Find(x => x.GetSystemLanguage() == systemLanguage);

            return found ?? currentLanguage;
        }

        private Local_Language GetNext(int dir)
        {
            int newIndex = Math_Utilities.LoopNumber(currentIndex + dir, 0, GetManagerData().GetLanguages().Count - 1);

            return GetManagerData().GetLanguages()[newIndex];
        }

        public Local_Language GetCurrent() => currentLanguage;

        public int IndexOf(Local_Language language) => GetManagerData().GetLanguages().IndexOf(language);

        public int LanguageCount() => GetManagerData().GetLanguages().Count;
    }
}