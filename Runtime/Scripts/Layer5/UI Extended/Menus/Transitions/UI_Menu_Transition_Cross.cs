#region

using System;
using System.Collections;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable]
    public class UI_Menu_Transition_Cross : UI_Menu_Transition_Time
    {
        public override IEnumerator Transition(MonoBehaviour mono, UI_Menu inMenu, UI_Menu outMenu)
        {
            mono.StartCoroutine(MenuOut(outMenu));
            yield return MenuIn(inMenu);
        }

        public override IEnumerator MenuOut(UI_Menu outMenu)
        {
            outMenu.GetVisbilityController().SetActive(true);

             outMenu.GetStateController().SetState(MenuState.ENABLING);

             outMenu.GetVisbilityController().SetEnabledAlpha(0);

             float t = 0;
             
             while (t < 1)
             {
                 t += (Time.deltaTime / time);

                 yield return null;
                 
                 outMenu.GetVisbilityController().SetEnabledAlpha(t);
             }
        }

        public override IEnumerator MenuIn(UI_Menu inMenu)
        {
            float t = 0;

            inMenu.GetStateController().SetState(MenuState.DISABLING);

            inMenu.GetVisbilityController().SetInteractable(false);

           inMenu.GetVisbilityController().SetEnabledAlpha(1);
            
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