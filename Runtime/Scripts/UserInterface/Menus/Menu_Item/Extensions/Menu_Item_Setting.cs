using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Item_Setting : Menu_Item_Extension
    {
        [SerializeField] private SettingsInterfaceType settingType;

        [ShowIf(nameof(settingType), SettingsInterfaceType.LOCAL), SerializeField]
        private Setting_Container reference;

        [ShowIf(nameof(settingType), SettingsInterfaceType.KEY), Dropdown(Settings_Manager.KEY), SerializeField]
        private string settingsKey;

        public override bool Spawn(RectTransform parent, UI_Menu menu)
        {
            UI_Menu_Config_SO config = menu.GetContentController().GetMenuConfig();

            if (!Settings_Manager.GetInstance().GetSetting(settingsKey, out Setting _foundSetting))
            {
                return false;
            }

            UI_Setting setting = config.GetConfig().GetSettingsPrefab(_foundSetting.GetSettingsType());

            UI_Setting settingInstance = Object.Instantiate(setting, parent);

            spawnedObject = settingInstance.gameObject;

            switch (settingType)
            {
                case SettingsInterfaceType.KEY:
                    settingInstance.Setup(settingsKey);
                    break;
                case SettingsInterfaceType.LOCALREFERENCE:
                    settingInstance.Setup(reference);
                    break;
            }

            settingInstance.UpdateUI();

            return true;
        }
    }
}