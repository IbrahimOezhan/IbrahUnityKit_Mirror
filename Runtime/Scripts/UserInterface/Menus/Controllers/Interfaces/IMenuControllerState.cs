namespace IbrahKit.UI
{
    public interface IMenuControllerState
    {
        public void Enable();

        public void Enable<T>(params object[] args) where T : Menu_Transition;

        public void Disable();

        public void Disable<T>(params object[] args) where T : Menu_Transition;

        public void Toggle();

        public void Toggle<T>(params object[] args) where T : Menu_Transition;

        public void Transition<T>(UI_Menu menuOut, bool allowBack = true, params object[] args) where T : Menu_Transition;

        public void SetState(MenuState state);

        public MenuState GetState();

        public MenuStateCompact GetCompactState();
    }
}