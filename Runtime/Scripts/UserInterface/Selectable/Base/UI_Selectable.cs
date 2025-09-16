using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IbrahKit
{
    public class UI_Selectable : UI_Base, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ICursorHandler
    {
        private UI_Selectable_CursorInput cursorInput = new();

        [SerializeField]
        private UI_Selectable_StateController stateController;

        [SerializeField]
        private UI_Selectable_TransitionController transitionController;

        [SerializeField]
        private UI_Audio_Config_SO overrideAudio;

        [ReadOnly, SerializeField]
        private UI_Selectable_Group selectableGroup;

        protected override void Awake()
        {
            base.Awake();

            stateController.GetOnStateChangedEvent().AddListener(Visualize);

            transform.BetterTryGetComponentInParent(out selectableGroup);

            cursorInput.Init(stateController, this);

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
            transitionController.Transition(state,stateController.GetInteractable());
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

        public bool DisallowPress()
        {
            return selectableGroup != null && stateController.GetState() == UI_SELECTABLE_STATE.PRESSED;
        }

        public bool DisallowPressOnUp()
        {
            return selectableGroup != null;
        }
    }
}