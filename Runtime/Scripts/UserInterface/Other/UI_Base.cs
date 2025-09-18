using UnityEngine;

namespace IbrahKit
{
    public abstract class UI_Base : MonoBehaviour, IMenuUpdate
    {
        [SerializeField] private UI_Menu parentMenu;

        protected virtual void Awake()
        {
            parentMenu = transform.BetterGetComponentInParent<UI_Menu>();

            if (parentMenu == null)
            {
                Debug.LogError("UI Menu missing");
                return;
            }

            parentMenu.GetContentController().AddUI(this);
        }

        protected virtual void Start()
        {
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }

        protected virtual void OnValidate()
        {
        }

        protected virtual void OnDestroy()
        {
            parentMenu.GetContentController().RemoveUI(this);
        }

        public UI_Menu GetParentMenu()
        {
            return parentMenu;
        }

        public abstract void OnMenuElementAdded();

        public abstract void OnMenuItemsInitialized();
    }
}