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

            Debug.Log(menuIn != null);
            Debug.Log(menuOut != null);

            if (outExists) menuOut.GetVisbilityController().SetInteractable(true);
            if (outExists) menuOut.GetVisbilityController().SetAlpha(1);
            if (outExists) menuOut.GetVisbilityController().SetActive(true);
            if (outExists) menuOut.GetStateController().SetState(Menu_State_Controller.State.ENABLED);
            if (outExists) menuOut.GetStateController().SetPreviousMenu(backOverride ?? menuIn);

            yield return null;

            if (inExists) menuIn.GetVisbilityController().SetInteractable(false);
            if (inExists) menuIn.GetVisbilityController().SetAlpha(0);
            if (inExists) menuIn.GetVisbilityController().SetActive(false);
            if (inExists) menuIn.GetStateController().SetState(Menu_State_Controller.State.DISABLED);
        }
    }
}