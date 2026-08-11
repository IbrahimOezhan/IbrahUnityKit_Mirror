#region

using System;
using System.Collections;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable, CreateAssetMenu(fileName = "NewMenuCrossTransition", menuName = "IbrahKit/UI/Menu/Transition/Cross")]
    public class UI_Menu_Transition_Cross : UI_Menu_Transition_Time
    {
        public override IEnumerator Transition(MonoBehaviour mono, UI_Menu inMenu, UI_Menu outMenu)
        {
            mono.StartCoroutine(MenuOut(outMenu));
            yield return MenuIn(inMenu);
        }

        public override IEnumerator MenuOut(UI_Menu outMenu)
        {
            outMenu.SetActive(true);

            outMenu.SetState(MenuState.ENABLING);

            outMenu.SetEnabledAlpha(0);

            float t = 0;

            while (t < 1)
            {
                t += (Time.deltaTime / time);

                yield return null;

                outMenu.SetEnabledAlpha(t);
            }
        }

        public override IEnumerator MenuIn(UI_Menu inMenu)
        {
            float t = 0;

            inMenu.SetState(MenuState.DISABLING);

            inMenu.SetInteractable(false);

            inMenu.SetEnabledAlpha(1);

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