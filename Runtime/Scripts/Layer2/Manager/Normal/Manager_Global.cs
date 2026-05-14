namespace IbrahKit.Manager
{
    public abstract class Manager_Global<T> : Manager<T> where T : Manager_Global<T>
    {
        protected sealed override void Awake()
        {
            if (GetInstance() != null && GetInstance() != this)
            {
                Destroy(gameObject);

                return;
            }
            else
            {
                SetInstanceThis();

                transform.parent = null;

                DontDestroyOnLoad(gameObject);

                InstanceAwake();
            }
        }
    }
}