using UnityEngine;

public class Override_Class<T> : Override_Base<T> where T : class
{
    [SerializeField] private T baseValue;

    [SerializeField] private T overrideValue = null;

    public Override_Class(T baseValue)
    {
        this.baseValue = baseValue;
    }

    public override void SetOverride(T overrideValue)
    {
        this.overrideValue = overrideValue;
    }

    public override T GetValue()
    {
        if (overrideValue != null)
        {
            return overrideValue;
        }

        return baseValue;
    }

    public override bool IsOverride()
    {
        return overrideValue != null;
    }

    public override void ClearOverride()
    {
        overrideValue = null;
    }
}
