#region

using System;
using System.Collections;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable]
    public class Menu_Transition_Instant : Menu_Transition
    {
        public Menu_Transition_Instant(UI_Menu menuIn, UI_Menu menuOut) : base(menuIn, menuOut)
        {
        }

        protected override IEnumerator Transition(bool inExists, bool outExists)
        {
            if (outExists)
            {
                menuOut.GetVisbilityController().SetInteractable(true);
                menuOut.GetVisbilityController().SetEnabledAlpha(1);
                menuOut.GetVisbilityController().SetActive(true);
                menuOut.GetStateController().SetState(MenuState.ENABLED);
            }

            yield return null;

            if (inExists)
            {
                menuIn.GetStateController().SetState(MenuState.DISABLED);
                menuIn.GetVisbilityController().SetInteractable(false);
                menuIn.GetVisbilityController().SetEnabledAlpha(0);
                menuIn.GetVisbilityController().SetActive(false);
            }
        }
    }
}