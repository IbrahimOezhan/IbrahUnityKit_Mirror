#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public class Menu_Item_Button_Definition_Quit : Menu_Item_Button_Definition
    {
        protected override bool AfterSpawn(RectTransform parent, UI_Menu menu, Menu_Item_Button spawnedButton)
        {
            spawnedButton.GetSelectable().GetStateController().GetOnPressSuccess().AddListener(Application.Quit);

            return true;
        }
    }
}