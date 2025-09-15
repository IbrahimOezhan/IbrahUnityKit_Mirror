using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IbrahKit
{
    public class UI_Selectable : UI_Base, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ICursorHandler
    {
        private UI_Selectable_CursorInput cursorInput = new();

        [SerializeField]
        private UI_Selectable_StateController stateController;

        [TabGroup("Transition Settings"), SerializeReference]
        private List<UI_Selectable_Transition> transitions = new();

        [TabGroup("Transition Settings"), SerializeReference]
        private List<UI_Selectable_Transition> transitionsInteractable = new();

        [TabGroup("Transition Settings"), SerializeReference]
        private List<UI_Selectable_Transition> transitionsNotInteractable = new();

        [TabGroup("Runtime Data"), ReadOnly, SerializeField]
        private UI_Selectable_Group selectableGroup;

        [SerializeField]
        private UI_Audio_SO overrideAudio;

        protected override void Awake()
        {
            base.Awake();

            stateController.GetOnStateChangedEvent().AddListener(Visualize);

            transform.BetterTryGetComponentInParent(out selectableGroup);

            cursorInput.Init(stateController, selectableGroup);

            stateController.Init(this, selectableGroup);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (selectableGroup != null) selectableGroup.Add(this);

            Visualize(stateController.GetState());
        }

        protected override void OnDisable()
        {
            stateController.SetState(UI_SELECTABLE_STATE.NONE);

            if (selectableGroup != null) selectableGroup.Remove(this);

            stateController.PressedStop();
        }

        public void Visualize(UI_SELECTABLE_STATE state)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].Apply(state, gameObject);
            }

            if (stateController.GetInteractable())
            {
                for (int i = 0; i < transitionsInteractable.Count; i++)
                {
                    transitionsInteractable[i].Apply(state, gameObject);
                }
            }
            else
            {
                for (int i = 0; i < transitionsNotInteractable.Count; i++)
                {
                    transitionsNotInteractable[i].Apply(state, gameObject);
                }
            }
        }

        public void SetInteractable(bool value)
        {
            stateController.SetInteractable(value);

            Visualize(stateController.GetState());
        }

        public UI_Selectable_StateController GetStateController() => stateController;

        public void OnPointerEnter(PointerEventData eventData) => cursorInput.OnPointerEnter(eventData);

        public void OnPointerExit(PointerEventData eventData) => cursorInput.OnPointerExit(eventData);

        public void OnPointerDown(PointerEventData eventData) => cursorInput.OnPointerDown(eventData);

        public void OnPointerUp(PointerEventData eventData) => cursorInput.OnPointerUp(eventData);
    }
}