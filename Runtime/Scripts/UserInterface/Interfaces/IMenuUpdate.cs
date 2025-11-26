using UnityEngine;

namespace IbrahKit.UI
{
    public interface IMenuUpdate : IMenuUpdateBase
    {
        public Transform transform { get; }

        public abstract void OnMenuInit(UI_Menu menu);
    }
}