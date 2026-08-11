#region

using System;
using System.Collections;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable,
     CreateAssetMenu(fileName = "NewMenuSequenceTransition", menuName = "IbrahKit/UI/Menu/Transition/Sequence")]
    public class UI_Menu_Transition_Sequence : UI_Menu_Transition_Time
    {
        public override IEnumerator Transition(MonoBehaviour mono, UI_Menu inMenu, UI_Menu outMenu)
        {
            yield return MenuIn(inMenu);
            yield return MenuOut(outMenu);
        }

        public override IEnumerator MenuOut(UI_Menu outMenu)
        {
            outMenu.SetActive(true);

            outMenu.SetState(MenuState.ENABLING);

            outMenu.SetEnabledAlpha(0);

            outMenu.SetInteractable(false);

            float t = 0;

            while (t < 1)
            {
                t += (Time.deltaTime / time);

                yield return null;

                outMenu.SetEnabledAlpha(t);
            }

            outMenu.SetState(MenuState.ENABLED);

            outMenu.SetInteractable(true);
        }

        public override IEnumerator MenuIn(UI_Menu inMenu)
        {
            inMenu.SetState(MenuState.DISABLING);

            inMenu.SetInteractable(false);

            inMenu.SetEnabledAlpha(1);

            float t = 0;

            while (t < 1)
            {
                t += (Time.deltaTime / time);

                yield return null;

                inMenu.SetEnabledAlpha(1 - t);
            }

            inMenu.SetState(MenuState.DISABLED);

            inMenu.SetActive(false);
        }
    }
}