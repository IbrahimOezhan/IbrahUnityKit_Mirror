namespace IbrahKit.Settings
{
    public interface ISettingConfig
    {
        public string GetKey();

        public bool TryGetInstance(out Setting_Base result);
    }
}