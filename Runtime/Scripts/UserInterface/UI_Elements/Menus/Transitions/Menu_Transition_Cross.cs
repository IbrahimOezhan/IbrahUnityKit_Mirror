using System.Collections;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Transition_Cross : Menu_Transition_Time
    {
        public Menu_Transition_Cross(UI_Menu menuIn, UI_Menu menuOut, float time = 1) : base(menuIn, menuOut, time)
        {
        }

        public override IEnumerator Transition(UI_Menu backOverride)
        {
            bool inExists = menuIn != null;
            bool outExists = menuOut != null;

            float t = 0;

            if (outExists) menuOut.GetVisbilityController().SetActive(true);

            if (outExists) menuOut.GetStateController().SetPreviousMenu(backOverride ?? menuIn);

            if (outExists) menuOut.GetStateController().SetState(UI_Menu_Controller_State.State.ENABLING);

            if (inExists) menuIn.GetStateController().SetState(UI_Menu_Controller_State.State.DISABLING);

            if (inExists) menuIn.GetVisbilityController().SetInteractable(false);

            if (inExists) menuIn.GetVisbilityController().SetAlpha(1);

            if (outExists) menuOut.GetVisbilityController().SetAlpha(0);

            while (t < 1)
            {
                t += (Time.deltaTime / time);

                yield return null;

                if (inExists) menuIn.GetVisbilityController().SetAlpha(1 - t);

                if (outExists) menuOut.GetVisbilityController().SetAlpha(t);
            }

            if (inExists) menuIn.GetStateController().SetState(UI_Menu_Controller_State.State.DISABLED);

            if (inExists) menuIn.GetVisbilityController().SetActive(false);

            if (outExists) menuOut.GetStateController().SetState(UI_Menu_Controller_State.State.ENABLED);

            if (outExists) menuOut.GetVisbilityController().SetInteractable(true);
        }
    }
}