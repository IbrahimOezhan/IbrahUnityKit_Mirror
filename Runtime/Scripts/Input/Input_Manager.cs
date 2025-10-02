using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace IbrahKit
{
    [DefaultExecutionOrder(Execution_Order.input)]
    public class Input_Manager : Manager_DDOL<Input_Manager>, IDebug
    {
        private ButtonControl lastPressed;

        [SerializeField, ReadOnly] private InputType currentInputType;

        public Action<InputType> OnInputChanged;

        private void Start()
        {
            Debug_Manager.Instance.Add(this);
        }

        private void Update()
        {
            InputType lastInputType = currentInputType;

            for (int i = 0; i < InputSystem.devices.Count; i++)
            {
                foreach (InputControl control in InputSystem.devices[i].allControls)
                {
                    if (control is ButtonControl button && button.wasPressedThisFrame)
                    {
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