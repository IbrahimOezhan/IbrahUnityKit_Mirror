#region

using System.Collections.Generic;
using IbrahKit.Settings;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class UI_Setting_Config : UI_Config
    {
        [SerializeField] private List<SettingsMap> settings;

        private struct SettingsMap
        {
            [SerializeField] private Setting setting;
            [SerializeField] private UI_Setting settingUI;
        }
    }
}