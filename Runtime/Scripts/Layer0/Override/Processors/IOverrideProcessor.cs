#region

using System.Collections.Generic;

#endregion

namespace IbrahKit.Override
{
    /// <summary>
    /// Interface used to determine what happens with elements when you add an Override
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOverrideProcessor<T>
    {
        /// <summary>
        /// Returns the override
        /// </summary>
        /// <param name="defaultValue">The default value to be used if the override was not set</param>
        /// <returns>Either the defaultValue or the override if one was set</returns>
        public T GetOverride(T defaultValue);
    }
}