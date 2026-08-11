#region

using System.Collections;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    public class UI_Menu_Transition_Sequence : UI_Menu_Transition_Time
    {
        public override IEnumerator Transition(MonoBehaviour mono, UI_Menu inMenu, UI_Menu outMenu)
        {
            yield return MenuIn(inMenu);
            yield return MenuOut(outMenu);
        }

        public override IEnumerator MenuOut(UI_Menu outMenu)
        {
            outMenu.GetVisbilityController().SetActive(true);

            outMenu.GetStateController().SetState(MenuState.ENABLING);

            outMenu.GetVisbilityController().SetEnabledAlpha(0);

            outMenu.GetVisbilityController().SetInteractable(false);
            
            float t = 0;

            while (t < 1)
            {
                t += (Time.deltaTime / time);

                yield return null;

                outMenu.GetVisbilityController().SetEnabledAlpha(t);
            }

            outMenu.GetStateController().SetState(MenuState.ENABLED);

            outMenu.GetVisbilityController().SetInteractable(true);
        }

        public override IEnumerator MenuIn(UI_Menu inMenu)
        {
            inMenu.GetStateController().SetState(MenuState.DISABLING);

            inMenu.GetVisbilityController().SetInteractable(false);

            inMenu.GetVisbilityController().SetEnabledAlpha(1);

            float t = 0;

            while (t < 1)
            {
                t += (Time.deltaTime / time);

                yield return null;

                inMenu.GetVisbilityController().SetEnabledAlpha(1 - t);
            }
            
            inMenu.GetStateController().SetState(MenuState.DISABLED);

            inMenu.GetVisbilityController().SetActive(false);
        }
    }
}