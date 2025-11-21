namespace IbrahKit.Settings
{
    public abstract class Setting_Base
    {
        public Setting_Base()
        {

        }

        public abstract bool Init(string initialValue);

        public abstract string GetValue();
    }
}
