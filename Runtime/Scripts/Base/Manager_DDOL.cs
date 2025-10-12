namespace IbrahKit
{
    public abstract class Manager_DDOL<T> : Manager<T> where T : Manager_DDOL<T>
    {
        protected override void Awake()
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

                OnAwake();
            }
        }
    }
}