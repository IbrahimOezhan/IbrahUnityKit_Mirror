#region

using System.Linq;
using IbrahKit.Input;
using IbrahKit.Manager;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.UI.Selectable
{
    public class UI_Selectable_Manager : Manager_Global<UI_Selectable_Manager>
    {
        private InputTypeNavigation currentType;

        private UI_Input input;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            if (Input_Manager.TryGet(out Input_Manager result))
            {
                result.OnInputChanged += OnInputChanged;

                result.InputUpdate();
            }

            input = new();

            input.Enable();

            if (IsSupported(Input_Manager.InputType.KEYBOARD))
            {
                input.Navigation.Move_Keyboard.performed += OnVectorInput;
                input.Navigation.Confirm_Keyboard.performed += ConfirmPerformed;
                input.Navigation.Confirm_Keyboard.canceled += ConfirmCanceled;
            }

            if (IsSupported(Input_Manager.InputType.GAMEPAD))
            {
                input.Navigation.Move_Gamepad.performed += OnVectorInput;
                input.Navigation.Confirm_Gamepad.performed += ConfirmPerformed;
                input.Navigation.Confirm_Gamepad.canceled += ConfirmCanceled;
            }
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            if (input != null)
            {
                if (IsSupported(Input_Manager.InputType.KEYBOARD))
                {
                    input.Navigation.Move_Keyboard.performed -= OnVectorInput;
                    input.Navigation.Confirm_Keyboard.performed -= ConfirmPerformed;
                    input.Navigation.Confirm_Keyboard.canceled -= ConfirmCanceled;
                }

                if (IsSupported(Input_Manager.InputType.GAMEPAD))
                {
                    input.Navigation.Move_Gamepad.performed -= OnVectorInput;
                    input.Navigation.Confirm_Gamepad.performed -= ConfirmPerformed;
                    input.Navigation.Confirm_Gamepad.canceled -= ConfirmCanceled;
                }

                input.Disable();

                input.Dispose();
            }

            if (Input_Manager.TryGet(out Input_Manager result))
            {
                result.OnInputChanged -= OnInputChanged;
            }
        }

        private void OnVectorInput(InputAction.CallbackContext context)
        {
            UI_Selectable_Controller_State.currentlySelected?.GetSelectable().GetNavigationController()
                .Navigate(context);
        }

        private void ConfirmPerformed(InputAction.CallbackContext context)
        {
            UI_Selectable_Controller_State.currentlySelected?.Pressed();
        }

        private void ConfirmCanceled(InputAction.CallbackContext context)
        {
            UI_Selectable_Controller_State.currentlySelected?.Select();
        }

        private void OnInputChanged(Input_Manager.InputType type)
        {
            if (!IsSupported(type))
            {
                return;
            }

            InputTypeNavigation newType = type switch
            {
                Input_Manager.InputType.GAMEPAD => InputTypeNavigation.BUTTONS,
                Input_Manager.InputType.KEYBOARD => InputTypeNavigation.BUTTONS,
                Input_Manager.InputType.MOUSE => InputTypeNavigation.POINT,
                Input_Manager.InputType.TOUCHSCREEN => InputTypeNavigation.POINT,
                _ => InputTypeNavigation.POINT,
            };

            if (currentType == newType)
            {
                return;
            }

            switch (newType)
            {
                case InputTypeNavigation.BUTTONS:

                    //if (activeSelectables.Count > 0 && activeSelectables[0] != null) activeSelectables[0].Select();

                    break;
                case InputTypeNavigation.POINT:

                    UI_Selectable_Controller_State.currentlySelected?.PressedStop();

                    break;
            }

            currentType = newType;
        }

        public bool IsSupported(Input_Manager.InputType type)
        {
            return Input_Manager.GetInstance().GetManagerData().EnabledInputMethods().Contains(type);
        }

        private enum InputTypeNavigation
        {
            POINT,
            BUTTONS,
        }
    }
}