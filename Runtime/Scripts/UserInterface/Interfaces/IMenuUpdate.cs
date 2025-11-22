using UnityEngine;

namespace IbrahKit
{
    public interface IMenuUpdate : IMenuUpdateBase
    {
        public Transform transform { get; }

        public abstract void OnMenuInit(UI_Menu menu);
    }
}