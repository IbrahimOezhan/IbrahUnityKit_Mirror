#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace IbrahKit.Override
{
    /// <summary>
    /// Implements IOverrideProcessor and returns the override with the highest priority
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OverridePriority<T> : IOverrideProcessor<T>
    {
        private T last;
        private readonly Dictionary<T, int> dict = new();
        
        public void AddOverride(int priority, T value)
        {
            dict.Add(value, priority);
            last = dict.OrderBy(x => x.Value).Last().Key;
        }

        public T GetOverride(T defaultValue)
        {
            return dict.Count > 0? last: defaultValue;
        }

        public void RemoveOverride(T key)
        {
            dict.Remove(key);
            last = dict.Count == 0? default : dict.OrderBy(x => x.Value).Last().Key;
        }
    }
}