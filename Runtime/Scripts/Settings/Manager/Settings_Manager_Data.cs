using IbrahKit.Settings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Settings_Manager_Data : ScriptableObject
{
    [SerializeField] private List<ISettingConfig> configs = new();

    public List<ISettingConfig> GetConfigs() => configs;

    public ISettingConfig GetConfig(string key) => configs.FirstOrDefault(x => x.GetKey().Equals(key));
}
