#region

using System;
using System.Collections;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable, CreateAssetMenu(fileName = "NewInstantTransition", menuName = "IbrahKit/UI/Menu/Transition/Instant")]
    public class UI_Menu_Transition_Instant : UI_Menu_Transition
    {
        public override IEnumerator Transition(MonoBehaviour mono, UI_Menu inMenu, UI_Menu outMenu)
        {
            mono.StartCoroutine(MenuOut(outMenu));
            mono.StartCoroutine(MenuIn(inMenu));
            yield break;
        }

        public override IEnumerator MenuOut(UI_Menu outMenu)
        {
            outMenu.SetInteractable(true);
            outMenu.SetEnabledAlpha(1);
            outMenu.SetActive(true);
            outMenu.SetState(MenuState.ENABLED);
            yield return null;
        }

        public override IEnumerator MenuIn(UI_Menu inMenu)
        {
            Debug.Log(10);

            inMenu.SetState(MenuState.DISABLED);

            Debug.Log(11);

            inMenu.SetInteractable(false);
            inMenu.SetEnabledAlpha(0);
            inMenu.SetActive(false);
            Debug.Log(12);

            yield return null;
        }
    }
}