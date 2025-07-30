using UnityEngine;

namespace IbrahKit
{
    [RequireComponent(typeof(UI_Interactive))]
    [AddComponentMenu("")]
    public abstract class UI_Extension : MonoBehaviour
    {
        private UI_Interactive uiInteractive;

        protected bool init;

        protected virtual void Awake()
        {
            Init();
        }

        protected virtual void OnDestroy()
        {

        }

        protected bool IsInitialized()
        {
            if (!init)
            {
                Init();
            }
            else
            {
                return true;
            }

            if (!init)
            {
                Debug.LogWarning("Could not initialize");

                return false;
            }

            Debug.Log("UI Extension Init Success", Color.green);

            return true;
        }

        protected virtual void Init()
        {
            if (uiInteractive == null && !TryGetComponent(out uiInteractive))
            {
                return;
            }

            init = true;
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