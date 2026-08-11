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
                Setting result = config.GetInstance();
                result.Set(GetValue(config.GetKey(),result.GetCurrent()));
                settingsInit.TryAdd(config.GetKey(), result);
            });
        }

        public float GetValue(string key, float defaultValue)
        {
            return saveData.GetKeyValues().GetValueOrDefault(key, defaultValue);
        }

        public bool TryGet(string key, out Setting setting)
        {
            return settingsInit.TryGetValue(key, out setting);
        }

        private class SaveData : ISavable
        {
            [JsonInclude] private Dictionary<string, float> settingValueMap = new();

            public Dictionary<string, float> GetKeyValues() => settingValueMap;
        }
    }
}