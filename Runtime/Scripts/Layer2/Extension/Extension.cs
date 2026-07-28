#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.Extension
{
    /// <summary>
    ///     A base class that aids in adding extensions of every kind.
    ///     To use it one must create a class that inherits from this, then add the Extension_Handler and close its generic
    ///     type with the newly created class
    /// </summary>
    [Serializable]
    public abstract class Extension
    {
        [SerializeField, ReadOnly] protected Extension_Handler_Base extension;
        protected bool init;

        protected Extension(Extension_Handler_Base extension)
        {
            if (extension == null)
            {
                Debug.Log("Extension handler attempted to set to null");
                return;
            }

            this.extension = extension;
        }

        public bool Init()
        {
            if (init) return true;

            if (!Application.isPlaying) return true;

            init = InitPro();

            return init;
        }

        public void Cleanup()
        {
            CleanupPro();
        }

        public void ResetInit()
        {
            init = false;
        }

        public void Run()
        {
            if (Init())
            {
                RunPro();
            }
        }

        public int GetOrder() => GetOrderPro();

        protected abstract bool InitPro();
        protected abstract int GetOrderPro();
        protected abstract void CleanupPro();
        protected abstract void RunPro();
    }
}