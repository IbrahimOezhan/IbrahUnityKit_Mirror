using System.Collections;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Transition_Cross : Menu_Transition
    {
        [SerializeField] float time;

        public Menu_Transition_Cross(UI_Menu menuIn, UI_Menu menuOut, float time = 1) : base(menuIn, menuOut)
        {
            this.time = time;
        }

        public override IEnumerator Transition(UI_Menu backOverride)
        {
            bool inExists = menuIn != null;
            bool outExists = menuOut != null;

            float t = 0;

            if (outExists) menuOut.GetVisbilityController().SetActive(true);

            if (outExists) menuOut.GetStateController().SetPreviousMenu(backOverride ?? menuIn);

            if (outExists) menuOut.GetStateController().SetState(Menu_State_Controller.State.ENABLING);

            if (inExists) menuIn.GetStateController().SetState(Menu_State_Controller.State.DISABLING);

            if (inExists) menuIn.GetVisbilityController().SetInteractable(false);

            while (t < 1)
            {
                t += (Time.deltaTime / time);

                yield return null;

                if (inExists) menuIn.GetVisbilityController().SetAlpha(1 - t);
                if (outExists) menuOut.GetVisbilityController().SetAlpha(t);
            }

            if (inExists) menuIn.GetStateController().SetState(Menu_State_Controller.State.DISABLED);

            if (inExists) menuIn.GetVisbilityController().SetActive(false);

            if (outExists) menuOut.GetStateController().SetState(Menu_State_Controller.State.ENABLED);

            if (outExists) menuOut.GetVisbilityController().SetInteractable(true);
        }
    }
}