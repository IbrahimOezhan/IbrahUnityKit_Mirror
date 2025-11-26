using IbrahKit.Save;
using IbrahKit.Settings;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;

namespace IbrahKit
{
    public class Settings_Manager : Manager_DDOL<Settings_Manager>
    {
        private const string SAVE_DATA_SETTINGS = "Settings";

        private SaveData saveData;

        private Dictionary<string, Setting_Base> settingsInit = new();

        [SerializeField] private UI_Menu menu;

        [SerializeField] private GlobalSettingsContainer settings;

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

            settings.GetConfigs().ForEach(x =>
            {
                if (x.TryGetInstance(out Setting_Base res))
                {
                    if (settingsInit.TryAdd(x.GetKey(), res)) ;
                    {

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

        public bool TryGet(string key, out Setting_Base setting)
        {
            return settingsInit.TryGetValue(key, out setting);
        }

        private class SaveData : Savable
        {
            [JsonInclude] private Dictionary<string, string> settingValueMap = new();

            public Dictionary<string, string> GetKeyValues() => settingValueMap;
        }
    }
}