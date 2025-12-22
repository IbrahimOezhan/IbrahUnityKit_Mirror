using System.Collections;

namespace IbrahKit.UI
{
    [System.Serializable]
    public class Menu_Transition_Instant : Menu_Transition
    {
        public Menu_Transition_Instant(UI_Menu menuIn, UI_Menu menuOut) : base(menuIn, menuOut) { }

        public override IEnumerator Transition()
        {
            bool inExists = menuIn != null;

            bool outExists = menuOut != null;

            if (outExists) menuOut.GetVisbilityController().SetInteractable(true);

            if (outExists) menuOut.GetVisbilityController().SetEnabledAlpha(1);

            if (outExists) menuOut.GetVisbilityController().SetActive(true);

            if (outExists) menuOut.GetStateController().SetState(MenuState.ENABLED);

            yield return null;

            if (inExists) menuIn.GetStateController().SetState(MenuState.DISABLED);

            if (inExists) menuIn.GetVisbilityController().SetInteractable(false);

            if (inExists) menuIn.GetVisbilityController().SetEnabledAlpha(0);

            if (inExists) menuIn.GetVisbilityController().SetActive(false);
        }
    }
}