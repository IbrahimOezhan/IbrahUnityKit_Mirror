namespace IbrahKit.UI
{
    public interface IConfigHolder
    {
        public bool TryGetConfig<TConfig>(out TConfig config) where TConfig : Config<TConfig>;
    }
}