namespace IbrahKit
{
    public abstract class Manager_Global<T> : Manager<T> where T : Manager_Global<T>
    {
        protected sealed override void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);

                return;
            }
            else
            {
                Instance = (T)this;

                transform.parent = null;

                DontDestroyOnLoad(gameObject);

                InstanceAwake();
            }
        }
    }
}