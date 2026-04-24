namespace IbrahKit.Settings
{
    public abstract class Setting_Number<TNumber> : Setting<TNumber>, ISettingNumber
    {
        public override bool Init(string initialValue)
        {
            return TrySetValue(initialValue);
        }

        public abstract void Decrement();

        public abstract void Increment();

        public abstract bool IsMax();

        public abstract bool IsMin();
    }
}