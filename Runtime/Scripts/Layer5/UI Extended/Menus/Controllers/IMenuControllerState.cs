namespace IbrahKit.UI.Menu
{
    public interface IMenuControllerState
    {
        public void Enable();

        public void Enable(UI_Menu_Transition transition);

        public void Disable();

        public void Disable(UI_Menu_Transition transition);

        public void Toggle();

        public void Toggle(UI_Menu_Transition transition);

        public void Transition(UI_Menu menuOut, UI_Menu_Transition transition,bool allowBack = true);

        public void SetState(MenuState state);

        public MenuState GetState();

        public MenuStateCompact GetCompactState();
    }
}