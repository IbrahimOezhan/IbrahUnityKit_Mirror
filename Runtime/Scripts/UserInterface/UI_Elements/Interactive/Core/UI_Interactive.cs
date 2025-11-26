namespace IbrahKit.UI
{
    public class UI_Interactive : Extension_Handler<UI_Interactive_Extension>, IMenuUpdate
    {
        UI_Menu menu;

        protected void OnDisable()
        {
            Cleanup();
        }

        public void OnMenuInit(UI_Menu menu)
        {
            this.menu = menu;
            InitExtensions();
            RunExtensions();
        }

        public UI_Menu GetMenu()
        {
            return menu;
        }
    }
}