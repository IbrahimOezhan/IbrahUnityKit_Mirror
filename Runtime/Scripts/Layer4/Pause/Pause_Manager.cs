#region

using System;
using IbrahKit.Debugging;
using IbrahKit.Manager;
using IbrahKit.State;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.Pause
{
    public class Pause_Manager : Manager_Global<Pause_Manager, Pause_Manager_Data>
    {
        private bool paused;

        private string stateBeforePause;

        private Pause_Input input;

        public Action<bool> OnPause;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            input = new();

            input.Enable();

            input.Map.Toggle.performed += Pause;
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            if (input != null)
            {
                input.Map.Toggle.performed -= Pause;

                input.Disable();
            }
        }

        public void Pause(InputAction.CallbackContext _context)
        {
            Pause();
        }

        public void Pause()
        {
            if (!State_Manager.TryGet(out State_Manager result))
            {
                return;
            }

            string currentState = result.GetCurrentState();

            Pause_Allow allow = GetManagerData().GetAllowPauses().Find(x => x.IsState(currentState));

            if (allow == null)
            {
                IbrahDebug.LogWarning("Allow is null");

                return;
            }

            if (!allow.Allow()) return;

            bool _paused = !paused;

            if (_paused)
            {
                stateBeforePause = currentState;

                result.SetCurrentState(GetManagerData().GetPausedKey());

                paused = _paused;
            }
            else if (!_paused)
            {
                result.SetCurrentState(stateBeforePause);

                paused = _paused;
            }

            Time.timeScale = paused ? 0 : 1;

            UpdatePause();
        }

        public void UpdatePause()
        {
            OnPause.Invoke(paused);
        }
    }
}