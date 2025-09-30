using UnityEngine;

namespace IbrahKit
{
    public abstract class UI_Base : MonoBehaviour, IMenuUpdate
    {
        private bool init;

        [SerializeField] private UI_Menu parentMenu;

        protected virtual void Awake()
        {
            if (!Init()) Debug.Log("Init failed");
        }

        protected virtual void Start()
        {
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void Update()
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

        private bool Init()
        {
            if (init) return true;

            parentMenu = transform.BetterGetComponentInParent<UI_Menu>();

            if (parentMenu == null)
            {
                Debug.LogError("UI Menu missing");
                return false;
            }

            parentMenu.GetContentController().AddUI(this);

            init = true;

            return true;
        }

        public UI_Menu GetParentMenu()
        {
            if (!Init()) Debug.LogWarning("Parent Menu is null");

            return parentMenu;
        }

        public abstract void MenuUpdate();

        public abstract void OnMenuEnabled();
    }
}