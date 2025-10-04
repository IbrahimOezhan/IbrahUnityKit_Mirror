using UnityEngine;

namespace IbrahKit
{
    public class Manager_Local<T> : MonoBehaviour where T : Manager_Local<T>
    {
        private static T Instance;

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

                OnAwake();
            }
        }

        protected virtual void OnAwake()
        {

        }
    }
}