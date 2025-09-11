using System.Collections;
using UnityEngine;

namespace IbrahKit
{
    public class Menu_Transition_Sequence : Menu_Transition_Time
    {
        public Menu_Transition_Sequence(UI_Menu menuIn, UI_Menu menuOut, float time = 1) : base(menuIn, menuOut, time)
        {
        }

        public override IEnumerator Transition(UI_Menu backOverride)
        {
            bool inExists = menuIn != null;
            bool outExists = menuOut != null;



            if (outExists) menuOut.GetVisbilityController().SetActive(true);

            if (outExists) menuOut.GetStateController().SetPreviousMenu(backOverride ?? menuIn);

            if (outExists) menuOut.GetStateController().SetState(Menu_State_Controller.State.ENABLING);

            if (outExists) menuOut.GetVisbilityController().SetAlpha(0);

            if (inExists) menuIn.GetStateController().SetState(Menu_State_Controller.State.DISABLING);

            if (inExists) menuIn.GetVisbilityController().SetInteractable(false);

            if (inExists) menuIn.GetVisbilityController().SetAlpha(1);

            float t = 0;

            while (t < 1 && inExists)
            {
                t += (Time.deltaTime / time);

                yield return null;

                menuIn.GetVisbilityController().SetAlpha(1 - t);
            }

            t = 0;

            while (t < 1 && outExists)
            {
                t += (Time.deltaTime / time);

                yield return null;

                menuOut.GetVisbilityController().SetAlpha(t);
            }

            if (inExists) menuIn.GetStateController().SetState(Menu_State_Controller.State.DISABLED);

            if (inExists) menuIn.GetVisbilityController().SetActive(false);

            if (outExists) menuOut.GetStateController().SetState(Menu_State_Controller.State.ENABLED);

            if (outExists) menuOut.GetVisbilityController().SetInteractable(true);
        }
    }
}