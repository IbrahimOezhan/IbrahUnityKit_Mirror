#region

using System;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable]
    public abstract class UI_Menu_Controller
    {
        private UI_Menu menu;

        public UI_Menu GetMenu() => menu;

        public void Init(UI_Menu menu)
        {
            this.menu = menu;
            OnInit();
        }

        protected abstract void OnInit();
        public abstract void OnMenuEnabled();
        public abstract void Lifecycle();
        public abstract void OnMenuDisabled();
    }
}