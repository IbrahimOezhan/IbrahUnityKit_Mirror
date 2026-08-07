#region

using IbrahKit.Core;
using IbrahKit.UI.Generic;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Selectable
{
    public partial class UI_Selectable : UnityCallbacks, IUIInit
    {
        [SerializeField] private UI_Selectable_Controller_State stateController;

        [SerializeField] private UI_Selectable_Controller_Transition transitionController;

        [SerializeField] private UI_Selectable_Controller_Navigation navigationController;

        [ReadOnly, SerializeField] private UI_Selectable_Group selectableGroup;

        private readonly UI_Selectable_Controller_Input_Cursor cursorInput = new();


        protected override void Update()
        {
            base.Update();

            if (selectableGroup) selectableGroup.Add(this);
        }

        protected override void OnDisable()
        {
            if (selectableGroup != null) selectableGroup.Remove(this);

            cursorInput.OnDisable();

            navigationController.OnDisable();

            transitionController.OnDisable();

            stateController.OnDisable();
        }

        public void OnMenuInitBottomUp()
        {
            transform.BetterTryGetComponentInParent(out selectableGroup);

            cursorInput.Init(this);

            navigationController.Init(this);

            transitionController.Init(this);

            stateController.Init(this);

            stateController.GetOnStateChangedEvent().AddListener(Visualize);

            Visualize(stateController.GetState());
        }

        public void OnMenuInitTopDown()
        {
        }

        protected override void Enable()
        {
            base.Enable();

            cursorInput.OnEnable();

            navigationController.OnEnable();

            transitionController.OnEnable();

            stateController.OnEnable();
        }

        public void Visualize(UI_SELECTABLE_STATE state, bool animate = true)
        {
            if (animate) transitionController.Transition(state, stateController.GetInteractable());
        }

        public void SetInteractable(bool value)
        {
            stateController.SetInteractable(value);

            Visualize(stateController.GetState());
        }

        public UI_Selectable_Controller_State GetStateController() => stateController;

        public UI_Selectable_Controller_Navigation GetNavigationController() => navigationController;

        public UI_Selectable_Group GetGroup() => selectableGroup;

        public RectTransform GetRectTransform() => transform as RectTransform;

        public bool DisallowPress() =>
            selectableGroup != null && stateController.GetState() == UI_SELECTABLE_STATE.PRESSED;

        public bool DisallowPressOnUp() => selectableGroup != null;
    }
}