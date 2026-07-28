#region

#endregion

namespace IbrahKit.Override
{
    /// <summary>
    ///     Implements IOverrideProcessor and always returns the newest override
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OverrideReplace<T> : IOverrideProcessor<T>
    {
        private T overrideValue;
        private bool overriden;

        public T GetOverride(T defaultValue)
        {
            return overriden ? overrideValue : defaultValue;
        }

        public void AddOverride(T value)
        {
            overrideValue = value;
            overriden = true;
        }

        public void RemoveOverride()
        {
            overriden = false;
        }
    }
}