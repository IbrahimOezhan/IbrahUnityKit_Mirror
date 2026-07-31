#region

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Selectable
{
    [Serializable]
    public class UI_Selectable_Controller_Transition : UI_Selectable_Controller, ISelfValidator
    {
        [SerializeReference] private List<UI_Selectable_Transition> transitions = new();

        [SerializeReference] private List<UI_Selectable_Transition> transitionsInteractable = new();

        [SerializeReference] private List<UI_Selectable_Transition> transitionsNotInteractable = new();

        public void Validate(SelfValidationResult result)
        {
            if (transitions.Count == 0 && transitionsInteractable.Count == 0 && transitionsNotInteractable.Count == 0)
            {
                result.AddWarning("The selectable has no transitions");
            }
        }

        protected override void Init()
        {
            GameObject selectableObject = GetSelectable().gameObject;

            transitions.ForEach(x => x.Init(selectableObject));

            transitionsInteractable.ForEach(x => x.Init(selectableObject));

            transitionsNotInteractable.ForEach(x => x.Init(selectableObject));
        }

        public override void OnDisable()
        {
        }

        public override void OnEnable()
        {
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
    }
}