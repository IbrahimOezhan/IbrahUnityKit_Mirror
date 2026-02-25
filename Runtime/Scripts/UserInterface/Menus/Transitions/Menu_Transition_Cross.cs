using System.Collections;
using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public class Menu_Transition_Cross : Menu_Transition_Time
    {
        public Menu_Transition_Cross(UI_Menu menuIn, UI_Menu menuOut, float time = 1) : base(menuIn, menuOut, time)
        {
        }

        protected override IEnumerator Transition(bool inExists, bool outExists)
        {
            float t = 0;

            if (outExists) menuOut.GetVisbilityController().SetActive(true);

            if (outExists) menuOut.GetStateController().SetState(MenuState.ENABLING);

            if (inExists) menuIn.GetStateController().SetState(MenuState.DISABLING);

            if (inExists) menuIn.GetVisbilityController().SetInteractable(false);

            if (inExists) menuIn.GetVisbilityController().SetEnabledAlpha(1);

            if (outExists) menuOut.GetVisbilityController().SetEnabledAlpha(0);

            while (t < 1)
            {
                t += (Time.deltaTime / time);

                yield return null;

                if (inExists) menuIn.GetVisbilityController().SetEnabledAlpha(1 - t);

                if (outExists) menuOut.GetVisbilityController().SetEnabledAlpha(t);
            }

            if (inExists) menuIn.GetStateController().SetState(MenuState.DISABLED);

            if (inExists) menuIn.GetVisbilityController().SetActive(false);

            if (outExists) menuOut.GetStateController().SetState(MenuState.ENABLED);

            if (outExists) menuOut.GetVisbilityController().SetInteractable(true);
        }
    }
}