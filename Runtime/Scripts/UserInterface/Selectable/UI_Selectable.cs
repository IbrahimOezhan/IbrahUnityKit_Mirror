using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IbrahKit
{
    public class UI_Selectable : UI_Base, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ICursorHandler
    {
        [SerializeField, ReadOnly] private bool initialized;

        private UI_Selectable_Input_Cursor cursorInput = new();

        [SerializeField]
        private UI_Selectable_StateController stateController;

        [SerializeField]
        private UI_Selectable_TransitionController transitionController;

        [ReadOnly, SerializeField]
        private UI_Selectable_Group selectableGroup;

        protected override void Update()
        {
            base.Update();

            if (selectableGroup != null) selectableGroup.Add(this);
        }

        protected override void OnDisable()
        {
            stateController.SetState(UI_SELECTABLE_STATE.NONE);

            if (selectableGroup != null) selectableGroup.Remove(this);

            stateController.PressedStop();
        }

        private bool Init()
        {
            if (initialized) return true;

            cursorInput.Init(stateController, this);

            stateController.GetOnStateChangedEvent().AddListener(Visualize);

            transform.BetterTryGetComponentInParent(out selectableGroup);

            transitionController.Init(gameObject);

            stateController.Init(this, selectableGroup);

            initialized = true;

            return true;
        }

        public void Visualize(UI_SELECTABLE_STATE state)
        {
            transitionController.Transition(state, stateController.GetInteractable());
        }

        public void SetInteractable(bool value)
        {
            stateController.SetInteractable(value);

            Visualize(stateController.GetState());
        }

        public override void MenuUpdate()
        {
            if (!Init())
            {
                Debug.Log("Init failed");
                return;
            }
        }

        public override void OnMenuEnabled()
        {
            if(!Init())
            {
                Debug.Log("Init failed");
                return;
            }

            Visualize(stateController.GetState());
        }

        public UI_Selectable_StateController GetStateController() => stateController;

        public void OnPointerEnter(PointerEventData eventData) => cursorInput.OnPointerEnter(eventData);

        public void OnPointerExit(PointerEventData eventData) => cursorInput.OnPointerExit(eventData);

        public void OnPointerDown(PointerEventData eventData) => cursorInput.OnPointerDown(eventData);

        public void OnPointerUp(PointerEventData eventData) => cursorInput.OnPointerUp(eventData);

        public bool DisallowPress() => selectableGroup != null && stateController.GetState() == UI_SELECTABLE_STATE.PRESSED;

        public bool DisallowPressOnUp() => selectableGroup != null;
    }
}