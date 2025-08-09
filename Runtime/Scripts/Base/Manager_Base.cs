using UnityEngine;

namespace IbrahKit
{
    public abstract class Manager_Base<T> : MonoBehaviour where T : Manager_Base<T>
    {
        public static T Instance;

        public static bool TryGet(out T result, bool throwWarnings = true)
        {
            result = Instance;

            if (result != null)
            {
                return true;
            }

            Debug.LogWarning($"Instance of type {nameof(T)} not assigned");

            result = FindAnyObjectByType<T>();

            if (result == null)
            {
                Debug.LogWarning($"FindAnyObjectByType couldn't find object of type {nameof(T)}");
            }

            return result != null;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = (T)this;
            DontDestroyOnLoad(gameObject);
            OnAwake();
        }

        protected virtual void OnAwake()
        {

        }
    }
}