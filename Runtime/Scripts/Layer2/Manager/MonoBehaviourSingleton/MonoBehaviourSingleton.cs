namespace IbrahKit.Manager
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviourSingletonBase<T> where T : MonoBehaviourSingleton<T>
    {
    }
}