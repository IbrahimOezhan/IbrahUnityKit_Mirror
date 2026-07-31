namespace IbrahKit.UI.Menu
{
    public interface IMenuControllerContent
    {
        public void RegisterUI(IMenuInit value);


        public UI_Menu_Controller_Canvas GetCanvasController();
    }
}