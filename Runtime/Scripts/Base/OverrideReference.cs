public class OverrideReference<T> where T : class
{
    private T baseValue;
    private T overrideValue = null;

    public OverrideReference(T baseValue)
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
        return overrideValue != null ? overrideValue : baseValue;
    }
}
