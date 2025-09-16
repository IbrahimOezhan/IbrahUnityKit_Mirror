using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class UI_Selectable_TransitionController
    {
        [SerializeReference]
        private List<UI_Selectable_Transition> transitions;
        [SerializeReference]
        private List<UI_Selectable_Transition> transitionsInteractable;
        [SerializeReference]
        private List<UI_Selectable_Transition> transitionsNotInteractable;

        public void Transition(UI_SELECTABLE_STATE state, bool interactable)
        {
            transitions.ForEach(i => i.Apply(state));

            if (interactable)
            {
                transitionsInteractable.ForEach(i => i.Apply(state));
            }
            else
            {
                transitionsNotInteractable.ForEach(i => i.Apply(state));
            }
        }
    }
}