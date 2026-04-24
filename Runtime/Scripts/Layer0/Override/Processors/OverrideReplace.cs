#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace IbrahKit.Override
{
    public class OverrideReplace<T> : IOverrideProcessor<T>
    {
        public T Get(Dictionary<object, T> keys)
        {
            return keys.First().Value;
        }

        public void Add(object source, T value, Dictionary<object, T> keys)
        {
            keys.Clear();
            keys.Add(source, value);
        }
    }
}