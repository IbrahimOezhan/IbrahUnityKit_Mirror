using UnityEngine;

namespace IbrahKit
{
    public abstract class Manager_DDOL<T> : MonoBehaviour where T : Manager_DDOL<T>
    {
        private static T Instance;

        public static bool TryGet(out T result, bool throwWarnings = true)
        {
            result = Instance;

            if (result != null)
            {
                return true;
            }

            if (throwWarnings) Debug.LogWarning($"Instance of type {typeof(T)} not assigned");

            result = FindAnyObjectByType<T>();

            if (result == null && throwWarnings)
            {
                Debug.LogWarning($"FindAnyObjectByType couldn't find object of type {typeof(T)}");
            }

            return result != null;
        }

        public static T GetInstance()
        {
            return Instance;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);

                return;
            }
            else
            {
                Instance = (T)this;

                if (transform.parent != null) transform.parent = null;

                DontDestroyOnLoad(gameObject);

                OnAwake();
            }
        }

        protected virtual void OnAwake()
        {

        }
    }
}