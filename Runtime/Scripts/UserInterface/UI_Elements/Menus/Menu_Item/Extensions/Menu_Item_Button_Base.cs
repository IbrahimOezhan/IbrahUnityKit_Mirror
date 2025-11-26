using IbrahKit.Debug;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public abstract class Menu_Item_Button_Base : Menu_Item_Base
    {
        private UI_Menu_Item_Button_Text spawnedButton;

        private string value;

        [SerializeField] private LocalType localType = LocalType.LOCALIZE;

        [SerializeField, ShowIf(nameof(localType), LocalType.LOCALIZE)] private Local_Key_Reference localizationKey;

        [SerializeField, ShowIf(nameof(localType), LocalType.STATIC)] private string staticText;

        protected sealed override bool TrySpawnPro(RectTransform parent, UI_Menu menu, out GameObject go)
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

            spawnedButton.Initialize(value);

            go = spawnedButton.gameObject;

            return TrySpawnProPro(parent, menu, spawnedButton);
        }

        protected abstract bool TrySpawnProPro(RectTransform parent, UI_Menu menu, UI_Menu_Item_Button_Text spawnedButton);

        private enum LocalType
        {
            LOCALIZE,
            STATIC
        }
    }
}