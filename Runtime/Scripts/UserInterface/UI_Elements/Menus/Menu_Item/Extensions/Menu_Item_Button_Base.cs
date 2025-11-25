using IbrahKit.Debug;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Item_Button_Base : Menu_Item_Base
    {
        protected UI_Menu_Item_Button_Text spawnedButton;

        protected string value;

        [SerializeField] private LocalType localType = LocalType.LOCALIZE;

        [SerializeField, ShowIf(nameof(localType), LocalType.LOCALIZE)] private Local_Key_Reference localizationKey;

        [SerializeField, ShowIf(nameof(localType), LocalType.STATIC)] private string staticText;

        protected override bool TrySpawnPro(RectTransform parent, UI_Menu menu, out GameObject go)
        {
            go = null;

            UI_Menu_Config menuConfig = menu.GetContentController().GetMenuConfig();

            if (menuConfig == null) return false;

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
                IbrahDebug.LogWarning("Button could not be spawned");
                return false;
            }

            go = spawnedButton.gameObject;

            return true;
        }

        private enum LocalType
        {
            LOCALIZE,
            STATIC
        }
    }
}