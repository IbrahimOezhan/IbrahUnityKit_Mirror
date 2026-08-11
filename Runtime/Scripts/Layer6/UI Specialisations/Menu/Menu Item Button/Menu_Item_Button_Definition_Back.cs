#region

using System;
using IbrahKit.UI.Menu;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public class Menu_Item_Button_Definition_Back : Menu_Item_Button_Definition
    {
        protected override bool AfterSpawn(RectTransform parent, UI_Menu menu, Menu_Item_Button spawnedButton)
        {
            spawnedButton.GetSelectable().GetStateController().GetOnPressSuccess().AddListener(() =>
            {
                if (UI_Menu_Manager.TryGet(out UI_Menu_Manager result))
                {
                    result.TransitionBack(ScriptableObject.CreateInstance<UI_Menu_Transition_Instant>());
                }
            });

            return true;
        }
    }
}