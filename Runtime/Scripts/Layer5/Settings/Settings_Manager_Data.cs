#region

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

#endregion

namespace IbrahKit.Settings
{
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public class Settings_Manager_Data : ScriptableObject
    {
        [SerializeField] private List<Setting_Config> configs = new();

        public List<Setting_Config> GetConfigs() => configs;
    }
}