using System.Collections;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Transition_Instant : Menu_Transition
    {
        public Menu_Transition_Instant(UI_Menu menuIn, UI_Menu menuOut) : base(menuIn, menuOut)
        {

        }

        public override IEnumerator Transition(UI_Menu backOverride)
        {
            bool inExists = menuIn != null;
            bool outExists = menuOut != null;

            if (outExists) menuOut.GetStateController().SetPreviousMenu(backOverride ?? menuIn);

            if (inExists) menuIn.GetVisbilityController().SetInteractable(false);
            if (outExists) menuOut.GetVisbilityController().SetInteractable(true);

            yield return null;

            if (outExists) menuOut.GetVisbilityController().SetAlpha(1);
            if (inExists) menuIn.GetVisbilityController().SetAlpha(0);

            if (outExists) menuIn.GetStateController().SetState(Menu_State_Controller.State.DISABLED);
            if (inExists) menuOut.GetStateController().SetState(Menu_State_Controller.State.ENABLED);
        }
    }
}