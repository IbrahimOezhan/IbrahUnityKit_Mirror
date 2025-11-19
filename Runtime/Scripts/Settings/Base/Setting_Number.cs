namespace IbrahKit.Settings
{
    public abstract class Setting_Number<TNumber> : Setting<TNumber>
    {
        public Setting_Number(TNumber value)
        {
            TrySetValue(value);
        }

        public abstract void Increment();

        public abstract void Decrement();

        public abstract bool IsMin();

        public abstract bool IsMax();
    }
}