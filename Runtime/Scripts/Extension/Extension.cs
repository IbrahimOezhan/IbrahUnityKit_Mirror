using IbrahKit;
using UnityEngine;
using Debug = IbrahKit.Debug;

public abstract class Extension : MonoBehaviour
{
    protected bool init;

    public int GetOrder()
    {
        return Order();
    }

    protected bool IsInitialized()
    {
        if (!init)
        {
            Init();
        }
        else
        {
            return true;
        }

        if (!init)
        {
            Debug.LogWarning($"Could not initialize {this.GetType()} ({transform.GetTransformPath()})");

            return false;
        }

        Debug.Log("UI Extension Init Success", Color.green);

        return true;
    }

    public void ResetInit()
    {
        init = false;
    }

    protected abstract int Order();

    protected abstract void Init();
}
