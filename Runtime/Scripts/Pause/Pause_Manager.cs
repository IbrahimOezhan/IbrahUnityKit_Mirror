using IbrahKit.Debugging;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IbrahKit
{
    public class Pause_Manager : Manager_Global_Data<Pause_Manager, Pause_Manager_Data>
    {
        private bool paused;

        private string stateBeforePause;

        private UI_Input input;

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
            if(!State_Manager.TryGet(out State_Manager result))
            {
                return;
            }

            string currentState = result.GetCurrentState();

            AllowPause allow = GetManagerData().GetAllowPauses().Find(x => x.IsState(currentState));

            if(allow == null)
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