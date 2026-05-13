#region

using System;
using System.Collections.Generic;
using IbrahKit.Debugging;
using IbrahKit.InfoCollector;
using IbrahKit.Manager;
using IbrahKit.State;
using UnityEngine;

#endregion

namespace IbrahKit.Input
{
    public class Cursor_Visibilty_Manager : Manager_Global<Cursor_Visibilty_Manager>, IDebug
    {
        private bool isVisible;

        private InputType inputType;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            Input_Manager.GetInstance().OnInputChanged += OnInputTypeChanged;
            Input_Manager.GetInstance().InputUpdate();
        }

        private void Start()
        {
            Lifecycle_Diagnostics_Manager.GetInstance().Add(this);
        }

        private void Update()
        {
            switch (inputType)
            {
                case InputType.KEYBOARD:
                case InputType.MOUSE:
                    
                    if (Game_State_Manager.GetInstance() != null
                        && Game_State_Manager.GetInstance().GetStateMachine().GetState() is ICursorState cursorState)
                    {
                        isVisible = cursorState.ShowCursor();
                    }

                    break;

                default:

                    isVisible = false;

                    break;
            }
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            if (Input_Manager.GetInstance())
            {
                Input_Manager.GetInstance().OnInputChanged -= OnInputTypeChanged;
            }
        }

        public bool IsVisible()
        {
            return isVisible;
        }

        private void OnInputTypeChanged(InputType type)
        {
            inputType = type;
        }

        public string DebugContent()
        {
            return "Cursor Visibilty: " + IsVisible();
        }

        public int DebugOrder()
        {
            return -70;
        }
    }
}