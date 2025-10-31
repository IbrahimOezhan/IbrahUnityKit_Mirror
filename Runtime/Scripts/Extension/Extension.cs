using IbrahKit;
using UnityEngine;
using Debug = IbrahKit.Debug;

/// <summary>
/// A base class that aids in adding extensions of every kind. To use it one must create a class that inherits from this and then add the Extension_Handler and close its generic type with the newly created class
/// </summary>
public abstract class Extension : MonoBehaviour
{
    protected bool init;

    /// <summary>
    /// Get the order in which the extension is executed
    /// </summary>
    /// <returns>The order in which the extension is executed</returns>
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

    /// <summary>
    /// Resets the extensions initialized state
    /// </summary>
    public void ResetInit()
    {
        init = false;
    }

    protected abstract int Order();

    protected abstract void Init();
}
