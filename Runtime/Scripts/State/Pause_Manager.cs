using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IbrahKit
{
    public class Pause_Manager : Manager_DDOL<Pause_Manager>
    {
        private bool paused;

        private string stateBeforePause;

        private UI_Input input;

        [SerializeField, Dropdown(State_Manager.KEY)] private string pausedState;

        [SerializeField] private List<AllowPause> allowPause = new();

        [SerializeField] private UI_Menu_Basic Menu;

        public Action<bool> OnPause;

        protected override void OnAwake()
        {
            base.OnAwake();

            input = new();

            input.Enable();

            input.Map.Pause.performed += Pause;
        }

        private void OnDestroy()
        {
            if (input != null)
            {
                input.Map.Pause.performed -= Pause;

                input.Disable();
            }
        }

        public void Pause(InputAction.CallbackContext _context)
        {
            Pause();
        }

        public void Pause()
        {
            string currentState = State_Manager.Instance.GetCurrentState();

            AllowPause allow = allowPause.Find(x => x.IsState(currentState));

            if (!allow.Allow()) return;

            bool _paused = !paused;

            if (_paused)
            {
                Menu.Enable(null);

                stateBeforePause = currentState;

                State_Manager.Instance.SetCurrentState(pausedState);

                paused = _paused;
            }
            else if (!_paused && Menu.IsEnabled())
            {
                Menu.Disable();

                State_Manager.Instance.SetCurrentState(stateBeforePause);

                paused = _paused;
            }

            Time.timeScale = paused ? 0 : 1;

            UpdatePause();
        }

        public void UpdatePause()
        {
            OnPause.Invoke(paused);
        }

        [Serializable]
        private class AllowPause
        {
            [SerializeField] private bool allow;

            [Dropdown(State_Manager.KEY), SerializeField] private string state;

            public bool Allow()
            {
                return allow;
            }

            public bool IsState(string state)
            {
                return state.Equals(state);
            }
        }
    }
}