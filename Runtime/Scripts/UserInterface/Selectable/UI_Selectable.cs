using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace IbrahKit
{
    public class UI_Selectable : UI_Base, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ICursorHandler
    {
        [TabGroup("Transition Settings"), SerializeField]
        private SelectedState selectedState;

        [TabGroup("Transition Settings"), SerializeReference]
        private List<SelectableTransition> transitions = new();

        [TabGroup("Transition Settings"), SerializeReference]
        private List<SelectableTransition> transitionsInteractable = new();

        [TabGroup("Transition Settings"), SerializeReference]
        private List<SelectableTransition> transitionsNotInteractable = new();

        [TabGroup("Runtime Data"), ReadOnly, SerializeReference]
        private SelectableGroup selectableGroup;

        [TabGroup("Events"), SerializeField]
        public UnityEvent OnClickEvent;

        [TabGroup("Events"), SerializeField]
        public UnityEvent OnClickNotInteractableEvent;

        [TabGroup("Events"), SerializeField]
        public UnityEvent OnDeSelect;

        [SerializeField] private bool interactable = true;

        public Action OnClickAction;

        public static UI_Selectable currentlySelected;

        protected override void OnEnable()
        {
            base.OnEnable();

            selectableGroup = Transform_Utilities.GetParent<SelectableGroup>(transform);

            if (selectableGroup != null) selectableGroup.Add(this);

            Visualize();
        }

        protected override void OnDisable()
        {
            SetState(SelectedState.None);

            if (selectableGroup != null) selectableGroup.Remove(this);

            DeSelect();
        }

        public void Visualize()
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].Apply(selectedState, gameObject);
            }

            if (interactable)
            {
                for (int i = 0; i < transitionsInteractable.Count; i++)
                {
                    transitionsInteractable[i].Apply(selectedState, gameObject);
                }
            }
            else
            {
                for (int i = 0; i < transitionsNotInteractable.Count; i++)
                {
                    transitionsNotInteractable[i].Apply(selectedState, gameObject);
                }
            }
        }

        public virtual void DeSelect()
        {
            SetState(SelectedState.None);

            if (currentlySelected == this) currentlySelected = null;
        }

        public void Select()
        {
            SetState(SelectedState.Pressed);

            currentlySelected = this;

            if (selectableGroup != null) selectableGroup.OnSelect(this);

            if (interactable)
            {
                OnClickEvent.Invoke();

                UI_Manager.Instance.OnUIClick();
            }
            else
            {
                OnClickNotInteractableEvent.Invoke();
            }
        }

        public void Hover()
        {
            SetState(SelectedState.Hovering);

            if (interactable)
            {
                UI_Manager.Instance.OnUIHover();
            }
        }

        public void SetInteractable(bool value)
        {
            interactable = value;

            Visualize();
        }

        protected void SetState(SelectedState state)
        {
            if (selectedState == SelectedState.Pressed && state != SelectedState.Pressed)
            {
                OnDeSelect.Invoke();
            }

            selectedState = state;

            Visualize();
        }

        public bool GetInteractable()
        {
            return interactable;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (selectableGroup != null && selectedState == SelectedState.Pressed) return;

            Hover();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (selectableGroup != null && selectedState == SelectedState.Pressed) return;

            DeSelect();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (selectableGroup != null && selectedState == SelectedState.Pressed) return;

            Select();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (selectableGroup != null) return;

            DeSelect();
        }
    }
}