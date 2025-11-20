using UnityEngine;

namespace IbrahKit
{
    public abstract class UI_Base : MonoBehaviour, IMenuUpdate
    {
        private bool init;

        [SerializeField] private UI_Menu parentMenu;

        protected virtual void Awake()
        {
            if (!Init()) IbrahDebug.Log("Init failed");
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

            if (!transform.BetterTryGetComponentInParent<UI_Menu>(out parentMenu, true))
            {
                IbrahDebug.LogError($"UI Menu missing: {transform.GetTransformPath()}");
                return false;
            }

            parentMenu.GetContentController().AddUI(this);

            init = true;

            return true;
        }

        public UI_Menu GetParentMenu()
        {
            if (!Init())
            {
                IbrahDebug.LogWarning("Parent Menu is null");
            }

            return parentMenu;
        }

        public abstract void OnMenuElementChanged();

        public abstract void OnMenuEnabled();
    }
}