using UnityEngine;

namespace IbrahKit.UI
{
    public interface IMenuUpdate
    {
        public Transform transform { get; }

        public abstract void OnMenuInit(UI_Menu menu);
    }
}