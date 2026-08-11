#region

using UnityEngine;

#endregion

namespace IbrahKit.UI.Generic
{
    public interface IUIInit
    {
        public Transform transform { get; }

        public abstract void OnMenuInitBottomUp();
        public abstract void OnMenuInitTopDown();
    }
}