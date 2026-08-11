#region

using System;
using System.Collections;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable]
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
            outMenu.GetVisbilityController().SetInteractable(true);
            outMenu.GetVisbilityController().SetEnabledAlpha(1);
            outMenu.GetVisbilityController().SetActive(true);
            outMenu.GetStateController().SetState(MenuState.ENABLED);
            yield return null;
        }

        public override IEnumerator MenuIn(UI_Menu inMenu)
        {
            inMenu.GetStateController().SetState(MenuState.DISABLED);
            inMenu.GetVisbilityController().SetInteractable(false);
            inMenu.GetVisbilityController().SetEnabledAlpha(0);
            inMenu.GetVisbilityController().SetActive(false);
            yield return null;
        }
    }
}