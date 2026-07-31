#region

using IbrahKit.UI.Menu;

#endregion

namespace IbrahKit.UI
{
    public class UI_Settings_Menu : UI_Menu
    {
        public static UI_Settings_Menu Instance;

        protected override void Awake()
        {
            base.Awake();

            Instance = this;
        }
    }
}