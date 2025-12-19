using UnityEngine;

namespace IbrahKit.UI
{
    public interface IMenuContent
    {
        public void RegisterUI(IMenuUpdate value);

        public UI_Menu_Config GetMenuConfig();

        public Canvas GetCanvas();
    }
}