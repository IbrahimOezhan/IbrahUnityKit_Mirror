#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable]
    public class Menu_Item_Button_Definition_Menu : Menu_Item_Button_Definition
    {
        [SerializeField] private Menu_Change_Type changeType;

        [ShowIf(nameof(changeType), Menu_Change_Type.REFERENCE), SerializeField]
        private UI_Menu menuReference;

        protected override bool AfterSpawn(RectTransform parent, UI_Menu menu, Menu_Item_Button spawnedButton)
        {
            if (changeType == Menu_Change_Type.REFERENCE)
                spawnedButton.GetSelectable().GetStateController().GetOnPressSuccess().AddListener(() =>
                {
                    menu.GetStateController().Transition<Menu_Transition_Instant>(menuReference);
                });

            return true;
        }

        private enum Menu_Change_Type
        {
            REFERENCE,
        }
    }
}