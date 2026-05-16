#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public class Menu_Item_Quit : Menu_Item_Button_Base
    {
        protected override bool TrySpawnProPro(RectTransform parent, UI_Menu menu,
            UI_Menu_Item_Button_Text spawnedButton)
        {
            spawnedButton.GetSelectable().GetStateController().GetOnPressSuccess().AddListener(() =>
            {
                Application.Quit();
            });

            return true;
        }
    }
}