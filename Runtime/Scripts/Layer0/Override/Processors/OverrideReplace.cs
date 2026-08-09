#region

#endregion

using UnityEngine;

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

        // TODO: Figure out why the fuck the null check doesnt catch null values
        public void AddOverride(T value)
        {
            if (value == null)
            {
                Debug.Log("Value to be added was null");
                return;
            }
            
            overrideValue = value;
            overriden = true;
        }

        public void RemoveOverride()
        {
            overriden = false;
        }
    }
}