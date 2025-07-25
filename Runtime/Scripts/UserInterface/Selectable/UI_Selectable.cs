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

        [TabGroup("Events"), SerializeField]
        public UnityEvent OnClickEvent;

        [TabGroup("Events"), SerializeField]
        public UnityEvent OnClickNotInteractableEvent;

        [SerializeField] private bool interactable = true;

        public Action OnClickAction;

        public static UI_Selectable currentlySelected;

        protected override void OnEnable()
        {
            base.OnEnable();

            Visualize();
        }

        protected override void OnDisable()
        {
            SetState(SelectedState.None);

            DeSelect();
        }

        public virtual void Select()
        {
            SetState(SelectedState.Hovering);

            if (currentlySelected != null) currentlySelected.DeSelect();

            currentlySelected = this;
        }

        public virtual void DeSelect()
        {
            SetState(SelectedState.None);

            if (currentlySelected == this) currentlySelected = null;
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

        public void Press()
        {
            SetState(SelectedState.Pressed);

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

        public void Exit()
        {
            SetState(SelectedState.None);
        }

        public void SetInteractable(bool value)
        {
            interactable = value;

            Visualize();
        }

        protected void SetState(SelectedState state)
        {
            selectedState = state;

            Visualize();
        }

        public bool GetInteractable()
        {
            return interactable;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Hover();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Exit();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Press();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Exit();
        }
    }
}