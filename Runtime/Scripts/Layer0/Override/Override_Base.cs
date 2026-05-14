#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace IbrahKit.Override
{
    /// <summary>
    /// Generic class to provide functionality to override data
    /// </summary>
    /// <typeparam name="TType">The type to provide overrides for</typeparam>
    public abstract class Override_Base<TType>
    {
        private readonly IOverrideProcessor<TType> processor;

        [SerializeField] private TType baseValue;

        protected Override_Base(TType baseValue, IOverrideProcessor<TType> processor)
        {
            this.baseValue = baseValue;
            this.processor = processor;
        }

        public TType GetValue() => processor.GetOverride(baseValue);

        public IOverrideProcessor<TType> GetProcessor() => processor;
    } 
}