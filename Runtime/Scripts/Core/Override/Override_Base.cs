using UnityEngine;

public abstract class Override_Base<T>
{
    public abstract void SetOverride(T value);

    public abstract T GetValue();

    public abstract bool IsOverride();

    public abstract void ClearOverride();
}
