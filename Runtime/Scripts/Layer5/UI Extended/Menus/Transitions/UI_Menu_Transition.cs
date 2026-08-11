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
        public void Transition2(MonoBehaviour mono, UI_Menu inMenu, UI_Menu outMenu)
        {
            mono.StartCoroutine(Transition(mono, inMenu, outMenu));
        }

        public abstract IEnumerator Transition(MonoBehaviour mono, UI_Menu inMenu, UI_Menu outMenu);

        public abstract IEnumerator MenuOut(UI_Menu outMenu);
        public abstract IEnumerator MenuIn(UI_Menu inMenu);
    }
}