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
            Debug.Log("0");
            if (outExists) menuOut.GetVisbilityController().SetAlpha(0);
            Debug.Log("1");
            if (inExists) menuIn.GetStateController().SetState(Menu_State_Controller.State.DISABLING);
            Debug.Log("2");
            if (inExists) menuIn.GetVisbilityController().SetInteractable(false);
            if (outExists) menuOut.GetVisbilityController().SetInteractable(false);
            Debug.Log("3");
            if (inExists) menuIn.GetVisbilityController().SetAlpha(1);

            float t = 0;
            Debug.Log("4");
            while (t < 1 && inExists)
            {
                t += (Time.deltaTime / time);

                yield return null;

                menuIn.GetVisbilityController().SetAlpha(1 - t);
            }

            t = 0;
            Debug.Log("5");
            while (t < 1 && outExists)
            {
                t += (Time.deltaTime / time);

                yield return null;

                menuOut.GetVisbilityController().SetAlpha(t);
            }
            Debug.Log("6");
            if (inExists) menuIn.GetStateController().SetState(Menu_State_Controller.State.DISABLED);
            Debug.Log("7");
            if (inExists) menuIn.GetVisbilityController().SetActive(false);
            Debug.Log("8");
            if (outExists) menuOut.GetStateController().SetState(Menu_State_Controller.State.ENABLED);


            Debug.Log("9");
            if (outExists) menuOut.GetVisbilityController().SetInteractable(true);

            Debug.Log("10");
        }
    }
}