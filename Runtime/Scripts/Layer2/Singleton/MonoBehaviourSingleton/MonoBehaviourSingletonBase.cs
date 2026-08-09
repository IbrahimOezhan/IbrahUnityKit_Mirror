#region

using System.Text;
using IbrahKit.Debugging;
using UnityEngine;

#endregion

namespace IbrahKit.Manager
{
    public abstract class MonoBehaviourSingletonBase<T> : MonoBehaviour where T : MonoBehaviourSingletonBase<T>
    {
        private static T Instance;

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

        public static bool TryGet(out T result, bool throwWarnings = true)
        {
            StringBuilder builder = new($"Could not find an Instance of the Manager {typeof(T)}\n");

            result = Instance;

            if (result != null)
            {
                return true;
            }

            if (throwWarnings)
            {
                builder.AppendLine($"Instance of type {typeof(T)} not assigned");
            }

            result = FindAnyObjectByType<T>();

            if (result == null && throwWarnings)
            {
                builder.AppendLine($"FindAnyObjectByType couldn't find object of type {typeof(T)}");
            }

            if (!result && builder.Length > 0)
            {
                IbrahDebug.LogWarning(builder);
            }

            return result != null;
        }

        protected void SetInstanceThis()
        {
            Instance = (T)this;
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

        protected virtual void InstanceAwake()
        {
        }

        protected virtual void InstanceDestroy()
        {
        }
    }
}