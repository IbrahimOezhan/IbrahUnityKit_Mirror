using IbrahKit;
using UnityEngine;

public abstract class Setting_Number<TNumber> : Setting<TNumber>
{
    [SerializeField] protected TNumber min;
    [SerializeField] protected TNumber max;
    [SerializeField] protected TNumber increment;

    public abstract void Increment();

    public abstract void Decrement();
}
