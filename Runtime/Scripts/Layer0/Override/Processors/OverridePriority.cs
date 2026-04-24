#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace IbrahKit.Override
{
    public class OverridePriority<T> : IOverrideProcessor<(T, int)>
    {
        public void Add(object source, (T, int) value, Dictionary<object, (T, int)> keys)
        {
            keys.Add(source, value);
        }

        public (T, int) Get(Dictionary<object, (T, int)> keys)
        {
            return keys.Values.OrderBy(x => x.Item2).First();
        }
    }
}