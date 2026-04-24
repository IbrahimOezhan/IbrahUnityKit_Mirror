#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public class UI_Menu_Config : UI_Config
    {
        [SerializeField] private UI_Menu_Item_Button_Text menuButtonPrefab;

        [SerializeField] private UI_Menu_Item_Button_Text staticMenuButtonPrefab;

        public UI_Menu_Item_Button_Text GetMenuButton() => menuButtonPrefab;

        public UI_Menu_Item_Button_Text GetMenuButtonStatic() => staticMenuButtonPrefab;
    }
}