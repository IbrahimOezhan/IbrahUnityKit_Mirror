using IbrahKit.Debugging;
using System.Collections;
using UnityEngine;

namespace IbrahKit.UI
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

            if (outExists) menuOut.GetStateController().SetState(UI_Menu_Controller_State.State.ENABLING);
            IbrahDebug.Log("0");
            if (outExists) menuOut.GetVisbilityController().SetEnabledAlpha(0);
            IbrahDebug.Log("1");
            if (inExists) menuIn.GetStateController().SetState(UI_Menu_Controller_State.State.DISABLING);
            IbrahDebug.Log("2");
            if (inExists) menuIn.GetVisbilityController().SetInteractable(false);
            if (outExists) menuOut.GetVisbilityController().SetInteractable(false);
            IbrahDebug.Log("3");
            if (inExists) menuIn.GetVisbilityController().SetEnabledAlpha(1);

            float t = 0;
            IbrahDebug.Log("4");
            while (t < 1 && inExists)
            {
                t += (Time.deltaTime / time);

                yield return null;

                menuIn.GetVisbilityController().SetEnabledAlpha(1 - t);
            }

            t = 0;
            IbrahDebug.Log("5");
            while (t < 1 && outExists)
            {
                t += (Time.deltaTime / time);

                yield return null;

                menuOut.GetVisbilityController().SetEnabledAlpha(t);
            }
            IbrahDebug.Log("6");
            if (inExists) menuIn.GetStateController().SetState(UI_Menu_Controller_State.State.DISABLED);
            IbrahDebug.Log("7");
            if (inExists) menuIn.GetVisbilityController().SetActive(false);
            IbrahDebug.Log("8");
            if (outExists) menuOut.GetStateController().SetState(UI_Menu_Controller_State.State.ENABLED);


            IbrahDebug.Log("9");
            if (outExists) menuOut.GetVisbilityController().SetInteractable(true);

            IbrahDebug.Log("10");
        }
    }
}