namespace IbrahKit.UI.Core.Config
{
    public interface IUIConfigHolder
    {
        public bool TryGetConfig<TConfig>(out TConfig config) where TConfig : UI_Config<TConfig>;
    }
}