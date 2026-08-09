#region

using System;
using System.Collections.Generic;
using IbrahKit.Core;
using IbrahKit.InfoCollector;
using IbrahKit.Manager;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

#endregion

namespace IbrahKit.Input
{
    [DefaultExecutionOrder(Execution_Order.input)]
    public class Input_Manager : MonoBehaviourSingletonDontDestroyOnLoadData<Input_Manager, Input_Manager_Data>, IInfoCollector
    {
        public enum InputType
        {
            KEYBOARD,
            MOUSE,
            GAMEPAD,
            TOUCHSCREEN,
        }

        [SerializeField, ReadOnly] private InputType currentInputType;

        private readonly HashSet<IInputType> observers = new();

        public Action<InputType> OnInputChanged;

        private ButtonControl lastPressed;

        private void Start()
        {
            Info_Collection_Manager.GetInstance().RegisterInfoCollector(this);
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

                    observers.ForEach(x => x.OnInput(currentInputType));

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

        public void Register(IInputType input)
        {
            observers.Add(input);
        }

        public void UnRegister(IInputType input)
        {
            observers.Remove(input);
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