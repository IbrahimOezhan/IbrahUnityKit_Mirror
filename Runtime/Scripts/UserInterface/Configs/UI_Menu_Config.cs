using IbrahKit.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class UI_Menu_Config
    {
        [SerializeField] private UI_Menu_Item_Button_Text menuButtonPrefab;
        [SerializeField] private UI_Menu_Item_Button_Text staticMenuButtonPrefab;
        [SerializeField] private List<SettingsMap> settings;

        public UI_Menu_Item_Button_Text GetMenuButton() => menuButtonPrefab;

        public UI_Menu_Item_Button_Text GetMenuButtonStatic() => staticMenuButtonPrefab;

        //public UI_Setting GetSettingsPrefab(Settings_Type settingsType) => settingPrefabs.Find(x => x.GetSettingsType() == settingsType);

        private struct SettingsMap
        {
            [SerializeField] private Setting_Base setting;
            [SerializeField] private UI_Setting settingUI;
        }
    }
}