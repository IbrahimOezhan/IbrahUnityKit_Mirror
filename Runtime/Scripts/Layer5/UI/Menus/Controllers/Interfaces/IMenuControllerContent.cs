namespace IbrahKit.UI
{
    public interface IMenuControllerContent
    {
        public void RegisterUI(IMenuUpdate value);

        public UI_Menu_Config GetMenuConfig();

        public UI_Menu_Controller_Canvas GetCanvasController();
    }
}