using UnityEngine;

namespace IbrahKit
{
    public interface IMenuUpdate : IMenuUpdateBase
    {
        public Transform transform { get; }

        public bool TryGetMenu(out UI_Menu menu)
        {
            return transform.BetterTryGetComponentInParent(out menu, true);
        }

        public void RegisterElement(UI_Menu menu)
        {
            menu.GetContentController().AddUI(this);
        }

        public void UnRegisterElement(UI_Menu menu)
        {
            menu.GetContentController().RemoveUI(this);
        }

        public void Init();

        public abstract bool IsInit();
    }
}