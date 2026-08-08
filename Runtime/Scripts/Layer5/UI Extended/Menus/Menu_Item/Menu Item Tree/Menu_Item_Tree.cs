#region

using System.Collections.Generic;
using IbrahKit.UI.Generic;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    public class Menu_Item_Tree : MonoBehaviour, IUIInit, IMenuReference
    {
        [Tooltip("List of predefined menu items."), SerializeReference]
        private List<Menu_Item_Base> listMenuItems = new();

        private UI_Menu menu;

        public UI_Menu GetMenu()
        {
            return menu;
        }

        public void OnMenuInitBottomUp()
        {
            foreach (Menu_Item_Base menuItem in listMenuItems)
            {
                if (TrySpawnMenuItem(menuItem, menu, out GameObject _))
                {
                    UI_Init.InitSubTree(transform);
                }
            }
        }

        public void OnMenuInitTopDown()
        {
            IMenuReference menuReference = transform.BetterGetComponentInParent<IMenuReference>();

            if (menuReference != null)
            {
                menu = menuReference.GetMenu();
            }
        }

        public bool TrySpawnMenuItem(Menu_Item_Base menuItem, UI_Menu menu, out GameObject result)
        {
            if (!menuItem.TrySpawn(transform as RectTransform, menu, out result)) return false;

            return result;
        }
    }
}