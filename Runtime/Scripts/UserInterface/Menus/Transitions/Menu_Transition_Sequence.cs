using System.Collections;
using UnityEngine;

namespace IbrahKit.UI
{
    public class Menu_Transition_Sequence : Menu_Transition_Time
    {
        public Menu_Transition_Sequence(UI_Menu menuIn, UI_Menu menuOut, float time = 1) : base(menuIn, menuOut, time)
        {
        }

        public override IEnumerator Transition()
        {
            bool inExists = menuIn != null;

            bool outExists = menuOut != null;

            if (outExists) menuOut.GetVisbilityController().SetActive(true);

            if (outExists) menuOut.GetStateController().SetState(MenuState.ENABLING);

            if (outExists) menuOut.GetVisbilityController().SetEnabledAlpha(0);

            if (inExists) menuIn.GetStateController().SetState(MenuState.DISABLING);

            if (inExists) menuIn.GetVisbilityController().SetInteractable(false);

            if (outExists) menuOut.GetVisbilityController().SetInteractable(false);

            if (inExists) menuIn.GetVisbilityController().SetEnabledAlpha(1);

            float t = 0;

            while (t < 1 && inExists)
            {
                t += (Time.deltaTime / time);

                yield return null;

                menuIn.GetVisbilityController().SetEnabledAlpha(1 - t);
            }

            t = 0;

            while (t < 1 && outExists)
            {
                t += (Time.deltaTime / time);

                yield return null;

                menuOut.GetVisbilityController().SetEnabledAlpha(t);
            }

            if (inExists) menuIn.GetStateController().SetState(MenuState.DISABLED);

            if (inExists) menuIn.GetVisbilityController().SetActive(false);

            if (outExists) menuOut.GetStateController().SetState(MenuState.ENABLED);

            if (outExists) menuOut.GetVisbilityController().SetInteractable(true);
        }
    }
}