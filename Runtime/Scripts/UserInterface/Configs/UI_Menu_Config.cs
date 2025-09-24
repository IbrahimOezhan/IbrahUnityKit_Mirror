using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class UI_Menu_Config
    {
        [SerializeField] private UI_Menu_Item_Button_Text menuButtonPrefab;
        [SerializeField] private UI_Menu_Item_Button_Text staticMenuButtonPrefab;
        [SerializeField] private List<UI_Setting> settingPrefabs;

        public UI_Menu_Item_Button_Text GetMenuButton()
        {
            return menuButtonPrefab;
        }

        public UI_Menu_Item_Button_Text GetMenuButtonStatic()
        {
            return staticMenuButtonPrefab;
        }

        public UI_Setting GetSettingsPrefab(SettingsType settingsType)
        {
            return settingPrefabs.Find(x => x.GetSettingsType() == settingsType);
        }
    }
}