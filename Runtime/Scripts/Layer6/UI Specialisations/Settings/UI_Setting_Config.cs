#region

using System;
using System.Collections.Generic;
using IbrahKit.Settings;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class UI_Setting_Config : Config<UI_Setting_Config>
    {
        [SerializeField] private List<SettingsMap> settings = new();

        [Serializable]
        private struct SettingsMap
        {
            [SerializeField] private Setting setting;
            [SerializeField] private UI_Setting settingUI;
        }
    }
}