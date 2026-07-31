#region

using System.Collections.Generic;
using IbrahKit.UI;
using UnityEngine;

#endregion

public class Menu_Item_Tree : MonoBehaviour, IMenuInit
{
    [Tooltip("List of predefined menu items."), SerializeReference]
    private List<Menu_Item_Base> listMenuItems = new();

    public void OnMenuInit(UI_Menu menu)
    {
        foreach (Menu_Item_Base menuItem in listMenuItems)
        {
            if (TrySpawnMenuItem(menuItem, menu, out GameObject _instance))
            {
                menu.GetContentController().RegisterUI(null);
            }
        }
    }

    public bool TrySpawnMenuItem(Menu_Item_Base menuItem, UI_Menu menu, out GameObject result)
    {
        if (!menuItem.TrySpawn(transform as RectTransform, menu, out result)) return false;

        return result;
    }
}