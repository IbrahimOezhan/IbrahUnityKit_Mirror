#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable]
    public class UI_Menu_Controller_State : UI_Menu_Controller, IMenuControllerState
    {
        [SerializeField, ReadOnly] private MenuState state = MenuState.DISABLED;

        public void Enable() => Enable(ScriptableObject.CreateInstance<UI_Menu_Transition_Instant>());

        public void Disable() => Disable(ScriptableObject.CreateInstance<UI_Menu_Transition_Instant>());

        public void Toggle() => Toggle(ScriptableObject.CreateInstance<UI_Menu_Transition_Instant>());

        public void Enable(UI_Menu_Transition transition)
        {
            if (UI_Menu_Manager.TryGet(out UI_Menu_Manager result, false))
            {
                result.SimpleStateChange(GetMenu(), MenuStateCompact.ENABLED, transition);
            }
        }

        public void Disable(UI_Menu_Transition transition) 
        {
            if (UI_Menu_Manager.TryGet(out UI_Menu_Manager result, false))
            {
                result.SimpleStateChange(GetMenu(), MenuStateCompact.DISABLED, transition);
            }
        }

        public void Toggle(UI_Menu_Transition transition) 
        {
            MenuStateCompact menuState = GetCompactState() == MenuStateCompact.ENABLED
                ? MenuStateCompact.DISABLED
                : MenuStateCompact.ENABLED;

            if (UI_Menu_Manager.TryGet(out UI_Menu_Manager result))
            {
                result.SimpleStateChange(GetMenu(), menuState, transition);
            }
        }

        public void Transition(UI_Menu menuOut, UI_Menu_Transition transition,bool allowBack = true)
        {
            if (UI_Menu_Manager.TryGet(out UI_Menu_Manager result, false))
            {
                result.Transition(GetMenu(), menuOut, transition,allowBack);
            }
        }

        public void SetState(MenuState state)
        {
            this.state = state;

            switch (GetCompactState())
            {
                case MenuStateCompact.ENABLED:

                    if (Application.isPlaying)
                    {
                        GetMenu().OnMenuEnabled();
                        GetMenu().GetMenuControllers().ForEach(x => x.OnMenuEnabled());
                    }

                    break;
                case MenuStateCompact.DISABLED:

                    if (Application.isPlaying)
                    {
                        GetMenu().OnMenuDisabled();
                        GetMenu().GetMenuControllers().ForEach(x => x.OnMenuDisabled());
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public MenuState GetState() => state;

        public MenuStateCompact GetCompactState() => state is MenuState.ENABLED or MenuState.ENABLING
            ? MenuStateCompact.ENABLED
            : MenuStateCompact.DISABLED;

        public void ToggleEditor(UI_Menu menu)
        {
            if (Application.isPlaying)
                return;

            bool menuEnabled = GetCompactState() == MenuStateCompact.ENABLED;

            UI_Menu_Transition.TransitionStatic(menuEnabled ? menu : null, menuEnabled ? null : menu);
        }
    }
}