#region

using System.Collections.Generic;

#endregion

namespace IbrahKit.Override
{
    public interface IOverrideProcessor<T>
    {
        public void Add(object source, T value, Dictionary<object, T> keys);

        public T Get(Dictionary<object, T> keys);
    }
}