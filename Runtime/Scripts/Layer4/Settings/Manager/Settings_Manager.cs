#region

using System.Collections.Generic;
using System.Text.Json.Serialization;
using IbrahKit.Manager;
using IbrahKit.Save;

#endregion

namespace IbrahKit.Settings
{
    public class Settings_Manager : MonoBehaviourSingletonDontDestroyOnLoadData<Settings_Manager, Settings_Manager_Data>
    {
        private readonly Dictionary<string, Setting> settingsInit = new();

        private SaveData saveData;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            saveData = Save_Manager.GetInstance().GetLoadedSave().Get<SaveData>();

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

        private class SaveData : ISavable
        {
            [JsonInclude] private Dictionary<string, string> settingValueMap = new();

            public Dictionary<string, string> GetKeyValues() => settingValueMap;
        }
    }
}