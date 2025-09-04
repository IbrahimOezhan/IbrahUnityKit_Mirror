namespace IbrahKit
{
    public interface IMenuContent
    {
        public void AddUI(IMenuUpdate value);
        public void RemoveUI(IMenuUpdate value);
        public UI_Menu_Config_SO GetMenuConfig();
    }
}