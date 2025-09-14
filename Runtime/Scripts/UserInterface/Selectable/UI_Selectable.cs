using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace IbrahKit
{
    public class UI_Selectable : UI_Base, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ICursorHandler
    {
        [TabGroup("Transition Settings"), SerializeField]
        private UI_SELECTABLE_STATE selectedState;

        [TabGroup("Transition Settings"), SerializeReference]
        private List<UI_Selectable_Transition> transitions = new();

        [TabGroup("Transition Settings"), SerializeReference]
        private List<UI_Selectable_Transition> transitionsInteractable = new();

        [TabGroup("Transition Settings"), SerializeReference]
        private List<UI_Selectable_Transition> transitionsNotInteractable = new();

        [TabGroup("Runtime Data"), ReadOnly, SerializeReference]
        private UI_Selectable_Group selectableGroup;

        [TabGroup("Events"), SerializeField]
        private UnityEvent OnClickEvent;

        [TabGroup("Events"), SerializeField]
        private UnityEvent OnClickNotInteractableEvent;

        [TabGroup("Events"), SerializeField]
        private UnityEvent OnDeSelect;

        [SerializeField] private UI_Audio_SO overrideAudio;

        [SerializeField] private bool interactable = true;

        public static UI_Selectable currentlySelected;

        protected override void OnEnable()
        {
            base.OnEnable();

            selectableGroup = transform.BetterGetComponentInParent<UI_Selectable_Group>();

            if (selectableGroup != null) selectableGroup.Add(this);

            Visualize();
        }

        protected override void OnDisable()
        {
            SetState(UI_SELECTABLE_STATE.None);

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
            SetState(UI_SELECTABLE_STATE.None);

            if (currentlySelected == this) currentlySelected = null;
        }

        public void Select()
        {
            SetState(UI_SELECTABLE_STATE.Pressed);

            currentlySelected = this;

            if (selectableGroup != null) selectableGroup.OnSelect(this);

            if (interactable)
            {
                OnClickEvent.Invoke();

                GetParentMenu().OnClick();
            }
            else
            {
                OnClickNotInteractableEvent.Invoke();
            }
        }

        public void Hover()
        {
            SetState(UI_SELECTABLE_STATE.Hovering);

            if (interactable)
            {
                GetParentMenu().OnHover();
            }
        }

        public void SetInteractable(bool value)
        {
            interactable = value;

            Visualize();
        }

        protected void SetState(UI_SELECTABLE_STATE state)
        {
            if (selectedState == UI_SELECTABLE_STATE.Pressed && state != UI_SELECTABLE_STATE.Pressed)
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
            if (selectableGroup != null && selectedState == UI_SELECTABLE_STATE.Pressed) return;

            Hover();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (selectableGroup != null && selectedState == UI_SELECTABLE_STATE.Pressed) return;

            DeSelect();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (selectableGroup != null && selectedState == UI_SELECTABLE_STATE.Pressed) return;

            Select();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (selectableGroup != null) return;

            DeSelect();
        }

        public UnityEvent GetOnClick()
        {
            return OnClickEvent;
        }

        public UnityEvent GetOnClickRefused()
        {
            return OnClickNotInteractableEvent;
        }

        public UnityEvent GetOnDeSelect()
        {
            return OnDeSelect;
        }
    }
}