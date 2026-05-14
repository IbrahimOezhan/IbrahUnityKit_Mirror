namespace IbrahKit.Override
{
    /// Override Class for Classes <br/> <inheritdoc/
    public class Override_Class<T> : Override_Base<T> where T : class
    {
        public Override_Class(T baseValue, IOverrideProcessor<T> processor) : base(baseValue, processor)
        {
        }
    }
}