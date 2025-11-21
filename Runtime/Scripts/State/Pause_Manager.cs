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

        [SerializeField] private UI_Menu menu;

        public Action<bool> OnPause;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            input = new();

            input.Enable();

            input.Map.Pause.performed += Pause;
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

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
            string currentState = State_Manager.GetInstance().GetCurrentState();

            AllowPause allow = allowPause.Find(x => x.IsState(currentState));

            if (!allow.Allow()) return;

            bool _paused = !paused;

            if (_paused)
            {
                menu.GetStateController().Enable();

                stateBeforePause = currentState;

                State_Manager.GetInstance().SetCurrentState(pausedState);

                paused = _paused;
            }
            else if (!_paused && menu.GetStateController().GetCompactState() == UI_Menu_Controller_State.StateCompact.ENABLED)
            {
                menu.GetStateController().Disable();

                State_Manager.GetInstance().SetCurrentState(stateBeforePause);

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