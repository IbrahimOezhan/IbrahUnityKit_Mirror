#region

using IbrahKit.UI;

#endregion

namespace IbrahKit
{
    public abstract class UI_Selectable_Controller
    {
        private UI_Selectable selectable;

        public void Init(UI_Selectable selectable)
        {
            this.selectable = selectable;
            Init();
        }

        protected abstract void Init();

        public abstract void OnEnable();

        public abstract void OnDisable();

        public UI_Selectable GetSelectable() => selectable;
    }
}