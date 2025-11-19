using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Item_Setting : Menu_Item_Base
    {

        protected override bool TrySpawnPro(RectTransform parent, UI_Menu menu, out GameObject go)
        {
            //UI_Menu_Config_SO config = menu.GetContentController().GetMenuConfig();

            //if (!Settings_Manager.GetInstance().GetSetting(settingsKey, out Setting _foundSetting))
            //{
            //    return false;
            //}

            //UI_Setting setting = config.GetConfig().GetSettingsPrefab(_foundSetting.GetSettingsType());

            //UI_Setting settingInstance = Object.Instantiate(setting, parent);

            //spawnedObject = settingInstance.gameObject;

            //switch (settingType)
            //{
            //    case Settings_Interface_Type.KEY:
            //        settingInstance.Setup(settingsKey);
            //        break;
            //    case Settings_Interface_Type.LOCALREFERENCE:
            //        settingInstance.Setup(reference);
            //        break;
            //}

            //settingInstance.UpdateUI();

            go = null;

            return true;
        }
    }
}