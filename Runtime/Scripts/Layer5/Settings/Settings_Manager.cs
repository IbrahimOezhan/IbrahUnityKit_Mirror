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

            Dictionary<string, float> dict = new();
            
            GetManagerData().GetConfigs().ForEach(config =>
            {
                float oldValue = GetValue(config.GetKey(), config.DefaultValue);
                
                Setting result = config.GetInstance(oldValue);

                settingsInit.Add(config.GetKey(), result);
                
                dict.Add(result.GetKey(), result.GetCurrent());
                
                result.onValueChanged += OnSettingUpdates;
            });
            
            saveData.settingValueMap = dict;
        }

        private void OnSettingUpdates(Setting setting)
        {
            saveData.settingValueMap[setting.GetKey()] = setting.GetCurrent();
        }
        
        public float GetValue(string key, float defaultValue)
        {
            return saveData.settingValueMap.GetValueOrDefault(key, defaultValue);
        }

        public bool TryGet(string key, out Setting setting)
        {
            return settingsInit.TryGetValue(key, out setting);
        }

        private class SaveData : ISavable
        {
            [JsonInclude] public Dictionary<string, float> settingValueMap = new();
        }
    }
}