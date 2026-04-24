#region

using System;
using IbrahKit.Core;
using IbrahKit.Debugging;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

#endregion

namespace IbrahKit.Input
{
    [DefaultExecutionOrder(Execution_Order.input)]
    public class Input_Manager : Manager_Global<Input_Manager>, IDebug
    {
        private ButtonControl lastPressed;

        [SerializeField, ReadOnly] private InputType currentInputType;

        public Action<InputType> OnInputChanged;

        private void Start()
        {
            Lifecycle_Diagnostics_Manager.GetInstance().Add(this);
        }

        private void Update()
        {
            InputType lastInputType = currentInputType;

            foreach (InputDevice device in InputSystem.devices)
            {
                foreach (InputControl control in device.allControls)
                {
                    if (!(control is ButtonControl button && button.wasPressedThisFrame))
                    {
                        continue;
                    }

                    switch (control.device)
                    {
                        case Mouse:
                            currentInputType = InputType.MOUSE;
                            lastPressed = button;
                            break;
                        case Gamepad:
                            currentInputType = InputType.GAMEPAD;
                            lastPressed = button;
                            break;
                        case Keyboard:
                            currentInputType = InputType.KEYBOARD;
                            lastPressed = button;
                            break;
                        case Touchscreen:
                            currentInputType = InputType.TOUCHSCREEN;
                            lastPressed = button;
                            break;
                    }

                    break;
                }
            }

            if (currentInputType != lastInputType) InputUpdate();
        }

        public InputType GetInputType()
        {
            return currentInputType;
        }

        public void InputUpdate()
        {
            OnInputChanged?.Invoke(currentInputType);
        }

        public string DebugContent()
        {
            return "Current Input Type: " + currentInputType + " Last Pressed: " + lastPressed.displayName;
        }

        public int DebugOrder()
        {
            return -90;
        }
    }
}