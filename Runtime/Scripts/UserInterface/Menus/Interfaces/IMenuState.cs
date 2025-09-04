using static IbrahKit.Menu_State_Controller;

namespace IbrahKit
{
    public interface IMenuState
    {
        public void Enable();

        public void Enable<T>(params object[] args) where T : Menu_Transition;

        public void Disable();

        public void Disable<T>(params object[] args) where T : Menu_Transition;

        public void Toggle();

        public void Toggle<T>(params object[] args) where T : Menu_Transition;

        public void Transition<T>(UI_Menu menuOut, UI_Menu backOverride = null, params object[] args) where T : Menu_Transition;

        public void TransitionToPrevious<T>(UI_Menu backOverride = null, params object[] args) where T : Menu_Transition;

        public void SetPreviousMenu(UI_Menu menu);

        public void SetState(State state);

        public State GetState();

        public StateCompact GetCompactState();
    }
}