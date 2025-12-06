using IbrahKit.Debugging;
using UnityEngine;

namespace IbrahKit
{
    public abstract class Manager<T> : MonoBehaviour where T : Manager<T>
    {
        private static T Instance;

        public static bool TryGet(out T result, bool throwWarnings = true)
        {
            result = Instance;

            if (result != null)
            {
                return true;
            }

            if (throwWarnings) IbrahDebug.LogWarning($"Instance of type {typeof(T)} not assigned");

            result = FindAnyObjectByType<T>();

            if (result == null && throwWarnings)
            {
                IbrahDebug.LogWarning($"FindAnyObjectByType couldn't find object of type {typeof(T)}");
            }

            return result != null;
        }

        protected void SetInstanceThis()
        {
            Instance = (T) this;
        }

        public static T GetInstance()
        {
            T result = Instance;

            if (result == null)
            {
                result = FindAnyObjectByType<T>();

                if (result == null)
                {
                    IbrahDebug.LogWarning($"FindAnyObjectByType couldn't find object of type {typeof(T)}");
                }
            }

            return result;
        }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);

                return;
            }
            else
            {
                Instance = (T)this;

                InstanceAwake();
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                InstanceDestroy();
            }
        }

        protected virtual void InstanceAwake()
        {

        }

        protected virtual void InstanceDestroy()
        {

        }
    }
}