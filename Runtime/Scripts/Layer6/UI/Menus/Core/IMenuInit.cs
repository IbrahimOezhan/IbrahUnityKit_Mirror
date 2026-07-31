#region

using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    public interface IMenuInit
    {
        public Transform transform { get; }

        public abstract void OnMenuInit(UI_Menu menu);
    }
}