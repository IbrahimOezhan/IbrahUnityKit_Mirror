using IbrahKit.UI;
using System.Linq;
using UnityEngine.InputSystem;

namespace IbrahKit
{
    public class UI_Navigation_Manager : Manager_Global<UI_Navigation_Manager, UI_Navigation_Manager_Data>
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

            if (GetManagerData().GetSupportedNavigationMethods().Contains(InputType.KEYBOARD))
            {
                input.Navigation.Move_Keyboard.performed += OnVectorInput;
                input.Navigation.Confirm_Keyboard.performed += ComfirmPerformed;
                input.Navigation.Confirm_Keyboard.canceled += ConfirmCanceled;
            }

            if (GetManagerData().GetSupportedNavigationMethods().Contains(InputType.GAMEPAD))
            {
                input.Navigation.Move_Gamepad.performed += OnVectorInput;
                input.Navigation.Confirm_Gamepad.performed += ComfirmPerformed;
                input.Navigation.Confirm_Gamepad.canceled += ConfirmCanceled;
            }
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            if (input != null)
            {
                if (GetManagerData().GetSupportedNavigationMethods().Contains(InputType.KEYBOARD))
                {
                    input.Navigation.Move_Keyboard.performed -= OnVectorInput;
                    input.Navigation.Confirm_Keyboard.performed -= ComfirmPerformed;
                    input.Navigation.Confirm_Keyboard.canceled -= ConfirmCanceled;
                }

                if (GetManagerData().GetSupportedNavigationMethods().Contains(InputType.GAMEPAD))
                {
                    input.Navigation.Move_Gamepad.performed -= OnVectorInput;
                    input.Navigation.Confirm_Gamepad.performed -= ComfirmPerformed;
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
            UI_Selectable_Controller_State.currentlySelected?.GetSelectable().GetNavigationController().Navigate(context);
        }

        private void ComfirmPerformed(InputAction.CallbackContext context)
        {
            UI_Selectable_Controller_State.currentlySelected?.Pressed();
        }

        private void ConfirmCanceled(InputAction.CallbackContext context)
        {
            UI_Selectable_Controller_State.currentlySelected?.Select();
        }

        private void OnInputChanged(InputType type)
        {
            if (!IsSupported(type))
            {
                return;
            }

            InputTypeNavigation newType = type switch
            {
                InputType.GAMEPAD => InputTypeNavigation.BUTTONS,
                InputType.KEYBOARD => InputTypeNavigation.BUTTONS,
                InputType.MOUSE => InputTypeNavigation.POINT,
                InputType.TOUCHSCREEN => InputTypeNavigation.POINT,
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

        public bool IsSupported(InputType type)
        {
            return GetManagerData().GetSupportedNavigationMethods().Contains(type);
        }

        private enum InputTypeNavigation
        {
            POINT,
            BUTTONS,
        }
    }
}