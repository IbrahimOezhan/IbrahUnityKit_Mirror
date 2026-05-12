namespace IbrahKit.Settings
{
    public interface ISettingConfig
    {
        public string GetKey();

        public string GetDefaultValue();

        public bool TryGetInstance(out Setting result);

        public Setting GetDummy();
    }
}