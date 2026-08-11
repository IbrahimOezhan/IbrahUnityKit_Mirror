using UnityEngine;

namespace IbrahKit.UI.Menu
{
    public partial class UI_Menu
    {
        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }

        protected virtual void OnDestroy()
        {
        }
    
        protected virtual void ObjectLifecycle()
        {
        }

        public virtual void OnMenuEnabled()
        {
            if (!UI_Menu_Manager.TryGet(out UI_Menu_Manager result)) return;
            result.OnHide += GU_Hide;
            result.InvokeHide();
        }

        protected virtual void BeforeInit()
        {
        }

        protected virtual void AfterInit()
        {
        }

        protected virtual void MenuLifecycle()
        {
        }

        public virtual void OnMenuDisabled()
        {
            if (UI_Menu_Manager.TryGet(out UI_Menu_Manager menu))
            {
                menu.OnHide -= GU_Hide;
            }
        }
    }
}

