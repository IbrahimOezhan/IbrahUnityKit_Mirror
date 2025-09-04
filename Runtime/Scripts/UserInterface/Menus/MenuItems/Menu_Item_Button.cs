using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Item_Button : Menu_Item_Base
    {
        protected UI_Menu_Button spawnedButton;
        protected string value;

        [SerializeField] private LocalType localType = LocalType.LOCALIZE;
        [SerializeField, Dropdown(Local_Manager.DROP), ShowIf(nameof(localType), LocalType.LOCALIZE)] private string localizationKey;
        [SerializeField, ShowIf(nameof(localType), LocalType.STATIC)] private string staticText;

        public override void Spawn(RectTransform parent, UI_Menu menu)
        {
            switch (localType)
            {
                case LocalType.LOCALIZE:
                    spawnedButton = Object.Instantiate(menu.GetContentController().GetMenuConfig().GetConfig().GetMenuButton(), parent);
                    value = localizationKey;
                    break;
                case LocalType.STATIC:
                    spawnedButton = Object.Instantiate(menu.GetContentController().GetMenuConfig().GetConfig().GetMenuButtonStatic(), parent);
                    value = staticText;
                    break;
            }


            spawnedObject = spawnedButton.gameObject;
        }

        private enum LocalType
        {
            LOCALIZE,
            STATIC
        }
    }
}