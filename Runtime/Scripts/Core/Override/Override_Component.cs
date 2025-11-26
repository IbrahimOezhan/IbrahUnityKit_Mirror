using IbrahKit.Debug;
using UnityEngine;

[System.Serializable]
public class Override_Component<T> : Override_Base<T> where T : Component
{
    [SerializeField] private T baseValue;
    [SerializeField] private T overrideValue = null;

    public Override_Component(T baseValue)
    {
        this.baseValue = baseValue;
    }

    public override void SetOverride(T value)
    {
        this.overrideValue = value;
    }

    public override T GetValue()
    {
        if (overrideValue != null)
        {
            IbrahDebug.Log(overrideValue);
            IbrahDebug.Log("Returning Override");
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
        this.overrideValue = null;
    }
}
