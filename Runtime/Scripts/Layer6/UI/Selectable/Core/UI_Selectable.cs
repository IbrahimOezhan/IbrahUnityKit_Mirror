#region

using System;
using System.Linq;
using IbrahKit.Core;
using IbrahKit.Input;
using IbrahKit.UI.Generic;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

#endregion

namespace IbrahKit.UI.Selectable
{
    public class UI_Selectable : UnityCallbacks, IUIInit, IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler, IRaycast_Receiver
    {
        [SerializeField] private UI_Selectable_Controller_State stateController;

        [SerializeField] private UI_Selectable_Controller_Transition transitionController;

        [ReadOnly, SerializeField] private UI_Selectable_Group selectableGroup;

        private readonly UI_Selectable_Controller_Input_Cursor cursorInput = new();

        private readonly UI_Selectable_Controller_Navigation navigationController = new();

        protected override void Update()
        {
            base.Update();

            if (selectableGroup != null) selectableGroup.Add(this);
        }

        protected override void OnDisable()
        {
            if (selectableGroup != null) selectableGroup.Remove(this);

            cursorInput.OnDisable();

            navigationController.OnDisable();

            transitionController.OnDisable();

            stateController.OnDisable();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (UI_Navigation_Manager.GetInstance().GetManagerData().GetSupportedNavigationMethods()
                .Contains(Input_Manager.InputType.MOUSE)) cursorInput.OnPointerDown(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (UI_Navigation_Manager.GetInstance().GetManagerData().GetSupportedNavigationMethods()
                .Contains(Input_Manager.InputType.MOUSE)) cursorInput.OnPointerEnter(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (UI_Navigation_Manager.GetInstance().GetManagerData().GetSupportedNavigationMethods()
                .Contains(Input_Manager.InputType.MOUSE)) cursorInput.OnPointerExit(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (UI_Navigation_Manager.GetInstance().GetManagerData().GetSupportedNavigationMethods()
                .Contains(Input_Manager.InputType.MOUSE)) cursorInput.OnPointerUp(eventData);
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
            throw new NotImplementedException();
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