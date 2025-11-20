namespace IbrahKit
{
    public interface IUnityCallbacks
    {
        public abstract void Awake();

        public abstract void Enable();

        public abstract void Start();

        public abstract void Disable();

        public abstract void Destroy();
    }
}