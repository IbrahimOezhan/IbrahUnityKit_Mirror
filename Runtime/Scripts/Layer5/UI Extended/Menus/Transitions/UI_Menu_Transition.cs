#region

using System;
using System.Collections;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable]
    public abstract class UI_Menu_Transition : ScriptableObject
    {
        public static void TransitionStatic(UI_Menu menuIn, UI_Menu menuOut)
        {
            bool inExists = menuIn != null;
            bool outExists = menuOut != null;

            if (outExists) menuOut.GetVisbilityController().SetActive(true);
            if (outExists) menuOut.GetVisbilityController().SetEnabledAlpha(1);
            if (outExists) menuOut.GetVisbilityController().SetInteractable(true);
            if (outExists) menuOut.GetStateController().SetState(MenuState.ENABLED);

            if (inExists) menuIn.GetVisbilityController().SetActive(false);
            if (inExists) menuIn.GetVisbilityController().SetEnabledAlpha(1);
            if (inExists) menuIn.GetVisbilityController().SetInteractable(false);
            if (inExists) menuIn.GetStateController().SetState(MenuState.DISABLED);
        }
        
        public abstract IEnumerator Transition(MonoBehaviour mono, UI_Menu inMenu, UI_Menu outMenu);

        public abstract IEnumerator MenuOut(UI_Menu outMenu);
        public abstract IEnumerator MenuIn(UI_Menu inMenu);
    }
}