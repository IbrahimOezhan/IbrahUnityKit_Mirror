namespace IbrahKit.Settings
{
    public interface ISettingNumber
    {
        public abstract void Increment();

        public abstract void Decrement();

        public abstract bool IsMin();

        public abstract bool IsMax();
    }
}