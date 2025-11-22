namespace IbrahKit
{
    public interface IMenuContent
    {
        public void RegisterUI(IMenuUpdate value);

        public UI_Menu_Config GetMenuConfig();
    }
}