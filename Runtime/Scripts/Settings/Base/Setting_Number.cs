using IbrahKit;
using UnityEngine;

namespace IbrahKit.Settings
{
    public abstract class Setting_Number<TNumber> : Setting<TNumber>
    {
        public abstract void Increment();

        public abstract void Decrement();

        public abstract bool IsMin();

        public abstract bool IsMax();
    }
}