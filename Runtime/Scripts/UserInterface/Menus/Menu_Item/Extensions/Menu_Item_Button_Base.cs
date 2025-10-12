using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Item_Button_Base : Menu_Item_Extension
    {
        protected UI_Menu_Item_Button_Text spawnedButton;
        protected string value;

        [SerializeField] private LocalType localType = LocalType.LOCALIZE;
        [SerializeField, Dropdown(Local_Manager.DROP), ShowIf(nameof(localType), LocalType.LOCALIZE)] private string localizationKey;
        [SerializeField, ShowIf(nameof(localType), LocalType.STATIC)] private string staticText;

        public override bool Spawn(RectTransform parent, UI_Menu menu)
        {
            UI_Menu_Config_SO menuConfigSO = menu.GetContentController().GetMenuConfig();

            if (menuConfigSO == null) return false;

            UI_Menu_Config menuConfig = menuConfigSO.GetConfig();

            switch (localType)
            {
                case LocalType.LOCALIZE:
                    spawnedButton = Object.Instantiate(menuConfig.GetMenuButton(), parent);
                    value = localizationKey;
                    break;
                case LocalType.STATIC:
                    spawnedButton = Object.Instantiate(menuConfig.GetMenuButtonStatic(), parent);
                    value = staticText;
                    break;
            }

            if (spawnedButton == null)
            {
                Debug.LogWarning("Button could not be spawned");
                return false;
            }

            spawnedObject = spawnedButton.gameObject;

            return true;
        }

        private enum LocalType
        {
            LOCALIZE,
            STATIC
        }
    }
}