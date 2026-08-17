#region

using System;
using IbrahKit.Core;
using IbrahKit.InfoCollector;
using IbrahKit.Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

#endregion

namespace IbrahKit.Input
{
    [DefaultExecutionOrder(Execution_Order.input)]
    public class Input_Manager : MonoBehaviourSingletonDontDestroyOnLoad<Input_Manager>, IInfoCollector
    {
        public enum InputType
        {
            KEYBOARD,
            MOUSE,
            GAMEPAD,
            TOUCHSCREEN,
        }

        private bool subcribed = false;

        [SerializeField, ReadOnly] private InputType currentInputType;

        public Action<InputType> OnInputChanged;

        private ButtonControl lastPressed;

        private void Start()
        {
            if (Info_Collection_Manager.TryGet(out Info_Collection_Manager result, false))
            {
                subcribed = true;
                result.RegisterInfoCollector(this);
            }
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();
            
            if(Info_Collection_Manager.TryGet(out Info_Collection_Manager result, subcribed))
                result.UnregisterInfoCollector(this);
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

        public string GetInformation()
        {
            return "Current Input Type: " + currentInputType + " Last Pressed: " + lastPressed.displayName;
        }

        public int GetDebugOrder()
        {
            return -90;
        }

        public InputType GetInputType()
        {
            return currentInputType;
        }

        public void InputUpdate()
        {
            OnInputChanged?.Invoke(currentInputType);
        }
    }
}