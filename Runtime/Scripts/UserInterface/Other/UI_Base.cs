using UnityEngine;

namespace IbrahKit
{
    public class UI_Base : MonoBehaviour, IMenuUpdate
    {
        [SerializeField] private UI_Menu_Basic parentMenu;

        protected virtual void Awake()
        {
            parentMenu = Transform_Utilities.GetParent<UI_Menu_Basic>(transform);

            if (parentMenu == null)
            {
                Debug.LogError("UI Menu missing");
                return;
            }

            parentMenu.AddUI(this);
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
            parentMenu.RemoveUI(this);
        }

        public void MenuUpdate()
        {

        }

        public UI_Menu_Basic GetParentMenu()
        {
            return parentMenu;
        }
    }
}