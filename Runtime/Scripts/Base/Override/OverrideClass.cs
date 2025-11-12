using UnityEngine;

public class OverrideClass<T> where T : class
{
    [SerializeField] private T baseValue;
    [SerializeField] private T overrideValue = null;

    public OverrideClass(T baseValue)
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

    public bool IsOverride()
    {
        return overrideValue != null;
    }

    public T GetValue()
    {
        if (overrideValue != null)
        {
            Debug.Log(overrideValue);
            Debug.Log("Returning Override");
            return overrideValue;
        }

        return baseValue;
    }
}
