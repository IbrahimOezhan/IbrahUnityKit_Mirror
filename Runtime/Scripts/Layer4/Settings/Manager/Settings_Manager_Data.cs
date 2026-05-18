#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using IbrahKit.Debugging;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.Settings
{
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public class Settings_Manager_Data : ScriptableObject
    {
        [SerializeField] private List<Setting_Config_Base> configs = new();

        public List<Setting_Config_Base> GetConfigs() => configs;
        
        public Setting_Config_Base GetConfig(string key) => configs.FirstOrDefault(x => x.GetKey().Equals(key));
    }
}