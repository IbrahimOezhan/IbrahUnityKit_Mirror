#region

using System;
using IbrahKit.Debugging;
using IbrahKit.Localization;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public abstract class Menu_Item_Button_Definition : Menu_Item_Base
    {
        [SerializeField] private LocalType localType = LocalType.LOCALIZE;

        [SerializeField, ShowIf(nameof(IsLoc))]
        private Local_Key localizationKey;

        [SerializeField, HideIf(nameof(IsLoc))]
        private string staticText;

        private bool IsLoc()
        {
            return localType is LocalType.LOCALIZE;
        }
        
        protected sealed override bool Spawn(RectTransform parent, UI_Menu menu, out GameObject go)
        {
            go = null;

            UI_Menu_Config menuConfig = menu.GetContentController().GetMenuConfig();

            if (menuConfig == null) return false;

            Menu_Item_Button button;
            
            switch (IsLoc())
            {
                case true:
                    button = Object.Instantiate(menuConfig.GetMenuButton(), parent);
                    button.Initialize(localizationKey);
                    break;
                case false:
                    button = Object.Instantiate(menuConfig.GetMenuButtonStatic(), parent);
                    button.Initialize(staticText);
                    break;
            }

            if (!button)
            {
                IbrahDebug.LogWarning("Button could not be spawned");
                return false;
            }
            

            go = button.gameObject;

            return AfterSpawn(parent, menu, button);
        }

        protected abstract bool AfterSpawn(RectTransform parent, UI_Menu menu,
            Menu_Item_Button spawnedButton);

        private enum LocalType
        {
            LOCALIZE,
            STATIC
        }
    }
}