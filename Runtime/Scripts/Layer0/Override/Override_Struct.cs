namespace IbrahKit.Override
{
    public class Override_Struct<T> : Override_Base<T> where T : struct
    {
        public Override_Struct(T baseValue, IOverrideProcessor<T> processor) : base(baseValue, processor)
        {
        }

        public override bool IsOverride()
        {
            return GetPairs().Count > 0;
        }
    }
}