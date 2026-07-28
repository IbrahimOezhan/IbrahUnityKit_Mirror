#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public class UI_Menu_Config : UI_Config
    {
        [SerializeField] private Menu_Item_Button menuButtonPrefab;

        [SerializeField] private Menu_Item_Button staticMenuButtonPrefab;

        public Menu_Item_Button GetMenuButton() => menuButtonPrefab;

        public Menu_Item_Button GetMenuButtonStatic() => staticMenuButtonPrefab;
    }
}