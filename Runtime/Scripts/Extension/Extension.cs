using System;
using UnityEngine;

namespace IbrahKit
{
    /// <summary>
    /// A base class that aids in adding extensions of every kind. To use it one must create a class that inherits from this and then add the Extension_Handler and close its generic type with the newly created class
    /// </summary>
    public abstract class Extension
    {
        protected bool init;

        protected GameObject go;

        protected Extension_Handler_Base extension;

        public Action runAllActions;

        public Extension(GameObject go)
        {
            this.go = go;
        }

        public bool Init()
        {
            if (init) return true;

            if (Application.isPlaying) return true;

            init = InitPro();

            return init;
        }

        protected abstract bool InitPro();

        public void Cleanup()
        {
            CleanupPro();
        }

        protected abstract void CleanupPro();

        public void ResetInit()
        {
            init = false;
        }

        public int GetOrder()
        {
            return GetOrderPro();
        }

        protected abstract int GetOrderPro();

        public void Run()
        {
            if (Init())
            {
                RunPro();
            }
        }

        protected abstract void RunPro();
    }
}