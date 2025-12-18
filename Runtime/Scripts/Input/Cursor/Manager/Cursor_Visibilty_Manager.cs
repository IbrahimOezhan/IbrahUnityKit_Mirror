using IbrahKit.Debugging;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    public class Cursor_Visibilty_Manager : Manager_Global<Cursor_Visibilty_Manager>, IDebug
    {
        private bool isVisible;

        private InputType inputType;

        [SerializeField] private List<CursorVisibilty> cursorVisibility;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            Input_Manager.GetInstance().OnInputChanged += OnInputTypeChanged;
            Input_Manager.GetInstance().InputUpdate();
        }

        private void Start()
        {
            Visual_Debug_Manager.GetInstance().Add(this);
        }

        private void Update()
        {
            switch (inputType)
            {
                case InputType.KEYBOARD:
                case InputType.MOUSE:

                    string state = string.Empty;

                    if (State_Manager.GetInstance() != null)
                    {
                        state = State_Manager.GetInstance().GetCurrentState();
                    }

                    isVisible = IsVisible(state);

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

        private bool IsVisible(string state)
        {
            for (int i = 0; i < cursorVisibility.Count; i++)
            {
                if (cursorVisibility[i].Match(state, out bool res))
                {
                    return res;
                }
            }

            return true;
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

        [System.Serializable]
        private class CursorVisibilty
        {
            [SerializeField] private State_Key state;
            [SerializeField] private bool visible;

            public bool Match(string state, out bool result)
            {
                result = visible;

                return this.state.Equals(state);
            }
        }
    }
}