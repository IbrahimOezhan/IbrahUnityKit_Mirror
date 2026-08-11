namespace IbrahKit.Manager
{
    public abstract class MonoBehaviourSingletonDontDestroyOnLoad<T> : MonoBehaviourSingletonBase<T>
        where T : MonoBehaviourSingletonDontDestroyOnLoad<T>
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