using IbrahKit;
using UnityEngine;

[System.Serializable]
public class OverrideComponent<T> where T : Component
{
    [SerializeField] private T baseValue;
    [SerializeField] private T overrideValue = null;

    public OverrideComponent(T baseValue)
    {
        this.baseValue = baseValue;
    }

    public void SetOverride(T overrideBaseSpeed)
    {
        this.overrideValue = overrideBaseSpeed;
    }

    public void ClearOverride()
    {
        this.overrideValue = null;
    }

    public T GetValue()
    {
        if (overrideValue != null)
        {
            IbrahDebug.Log(overrideValue);
            IbrahDebug.Log("Returning Override");
            return overrideValue;
        }

        return baseValue;
    }
}
