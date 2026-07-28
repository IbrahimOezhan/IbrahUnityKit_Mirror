namespace IbrahKit.Override
{
    /// Override Class for Structs
    /// <br />
    /// <inheritdoc />
    public class Override_Struct<TStruct> : Override_Base<TStruct> where TStruct : struct
    {
        public Override_Struct(TStruct baseValue, IOverrideProcessor<TStruct> processor) : base(baseValue, processor)
        {
        }
    }
}