using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class UI_Selectable_TransitionController : ISelfValidator
    {
        [SerializeReference]
        private List<UI_Selectable_Transition> transitions = new();
        [SerializeReference]
        private List<UI_Selectable_Transition> transitionsInteractable = new();
        [SerializeReference]
        private List<UI_Selectable_Transition> transitionsNotInteractable = new();

        public void Init(GameObject go)
        {
            transitions.ForEach(x => x.Init(go));
            transitionsInteractable.ForEach(x => x.Init(go));
            transitionsNotInteractable.ForEach(x => x.Init(go));
        }

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

        public void Validate(SelfValidationResult result)
        {
            if (transitions.Count == 0 && transitionsInteractable.Count == 0 && transitionsNotInteractable.Count == 0)
            {
                result.AddWarning("The selectable has no transitions");
            }
        }
    }
}