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

        public void Enable() => Enable<Menu_Transition_Instant>();

        public void Disable() => Disable<Menu_Transition_Instant>();

        public void Toggle() => Toggle<Menu_Transition_Instant>();

        public void Enable<T>(params object[] args) where T : Menu_Transition
        {
            if (UI_Menu_Manager.TryGet(out UI_Menu_Manager result, false))
            {
                result.SimpleStateChange<T>(GetMenu(), MenuStateCompact.ENABLED, args);
            }
        }

        public void Disable<T>(params object[] args) where T : Menu_Transition
        {
            if (UI_Menu_Manager.TryGet(out UI_Menu_Manager result, false))
            {
                result.SimpleStateChange<T>(GetMenu(), MenuStateCompact.DISABLED, args);
            }
        }

        public void Toggle<T>(params object[] args) where T : Menu_Transition
        {
            MenuStateCompact menuState = GetCompactState() == MenuStateCompact.ENABLED
                ? MenuStateCompact.DISABLED
                : MenuStateCompact.ENABLED;

            if (UI_Menu_Manager.TryGet(out UI_Menu_Manager result, true))
            {
                result.SimpleStateChange<T>(GetMenu(), menuState, args);
            }
        }

        public void Transition<T>(UI_Menu menuOut, bool allowBack = true, params object[] args)
            where T : Menu_Transition
        {
            if (UI_Menu_Manager.TryGet(out UI_Menu_Manager result, false))
            {
                result.Transition<T>(GetMenu(), menuOut, allowBack, args);
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
            }
        }

        public MenuState GetState() => state;

        public MenuStateCompact GetCompactState() => state == MenuState.ENABLED || state == MenuState.ENABLING
            ? MenuStateCompact.ENABLED
            : MenuStateCompact.DISABLED;

        protected override void OnInit()
        {
        }

        public override void OnMenuEnabled()
        {
        }

        public override void Lifecycle()
        {
        }

        public override void OnMenuDisabled()
        {
        }

        public void ToggleEditor(UI_Menu menu)
        {
            if (Application.isPlaying)
                return;

            bool menuEnabled = GetCompactState() == MenuStateCompact.ENABLED;

            Menu_Transition.Transition(menuEnabled ? menu : null, menuEnabled ? null : menu);
        }
    }
}