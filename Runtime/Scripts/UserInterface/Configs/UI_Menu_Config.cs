using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class UI_Menu_Config : UI_Config
    {
        [SerializeField] private UI_Menu_Item_Button_Text menuButtonPrefab;
        [SerializeField] private UI_Menu_Item_Button_Text staticMenuButtonPrefab;


        public UI_Menu_Item_Button_Text GetMenuButton() => menuButtonPrefab;

        public UI_Menu_Item_Button_Text GetMenuButtonStatic() => staticMenuButtonPrefab;

        //public UI_Setting GetSettingsPrefab(Settings_Type settingsType) => settingPrefabs.Find(x => x.GetSettingsType() == settingsType);
    }
}