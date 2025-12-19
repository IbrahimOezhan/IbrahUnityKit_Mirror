using IbrahKit.Save;
using IbrahKit.UI;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

namespace IbrahKit.Settings
{
    public class Settings_Manager : Manager_Global<Settings_Manager, Settings_Manager_Data>
    {
        private const string SAVE_DATA_SETTINGS = "Settings";

        private SaveData saveData;

        private readonly Dictionary<string, Setting> settingsInit = new();

        [SerializeField] private UI_Menu menu;

        public void OpenSettings(UI_Menu menu)
        {
            menu.GetStateController().Transition<Menu_Transition_Instant>(this.menu);
        }

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            if (Save_Manager.GetInstance().TryLoad(SAVE_DATA_SETTINGS, out saveData))
            {

            }

            GetManagerData().GetConfigs().ForEach(config =>
            {
                if (config.TryGetInstance(out Setting settingResult))
                {
                    if (settingsInit.TryAdd(config.GetKey(), settingResult))
                    {
                        settingResult.Init(config.GetDefaultValue());
                    }
                }
            });
        }

        public string GetValue(string key, string defaultValue)
        {
            if (saveData.GetKeyValues().TryGetValue(key, out string value))
            {
                return value;
            }

            return defaultValue;
        }

        public bool TryGet(string key, out Setting setting)
        {
            return settingsInit.TryGetValue(key, out setting);
        }

        private class SaveData : Savable
        {
            [JsonInclude]
            private Dictionary<string, string> settingValueMap = new();

            public Dictionary<string, string> GetKeyValues() => settingValueMap;
        }
    }
}