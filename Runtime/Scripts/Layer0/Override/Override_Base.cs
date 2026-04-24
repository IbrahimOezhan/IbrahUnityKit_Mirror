#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace IbrahKit.Override
{
    public abstract class Override_Base<T>
    {
        private readonly IOverrideProcessor<T> processor;

        [SerializeField] private T baseValue;

        [SerializeField] private Dictionary<object, T> overrideValue = new();

        public Override_Base(T baseValue, IOverrideProcessor<T> processor)
        {
            this.baseValue = baseValue;
            this.processor = processor;
        }

        public void SetOverride(object source, T value)
        {
            processor.Add(source, value, overrideValue);
        }

        public T GetValue()
        {
            return IsOverride() ? processor.Get(overrideValue) : baseValue;
        }

        public abstract bool IsOverride();

        public void ClearOverride()
        {
            GetPairs().Clear();
        }

        protected Dictionary<object, T> GetPairs() => overrideValue;
    }
}