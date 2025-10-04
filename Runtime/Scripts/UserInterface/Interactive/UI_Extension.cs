using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    [RequireComponent(typeof(UI_Interactive)), AddComponentMenu("")]
    public abstract class UI_Extension : MonoBehaviour
    {
        private UI_Interactive uiInteractive;

        [Button]
        private void DebugReset()
        {
            init = false;
        }

        protected bool init;

        protected virtual void Awake()
        {
            IsInitialized();
        }

        protected virtual void OnDestroy()
        {

        }

        protected bool IsInitialized()
        {
            if (init == true)
            {
                return true;
            }

            if (!TryInit())
            {
                Debug.LogWarning($"Could not initialize {this.GetType()} ({transform.GetTransformPath()})");
                return false;
            }

            bool playing = Application.isPlaying;

            if(playing) init = true;

            Debug.Log("UI Extension Init Success", Color.green);

            return true;
        }

        protected virtual bool TryInit()
        {
            return (uiInteractive != null || TryGetComponent(out uiInteractive) == true);
        }

        public virtual void Execute()
        {

        }

        public void UpdateUI()
        {
            if (!IsInitialized()) return;

            uiInteractive.UpdateUI();
        }

        public virtual int GetOrder()
        {
            return 0;
        }

        public void ResetInit()
        {
            init = false;
        }
    }
}