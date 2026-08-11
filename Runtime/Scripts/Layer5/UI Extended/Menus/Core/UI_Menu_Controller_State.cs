#region

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    public partial class UI_Menu
    {
        [SerializeField, ReadOnly] private MenuState state = MenuState.DISABLED;
        
        [SerializeField] private List<MenuTransition> transitions = new();
        
        public Action<bool> OnStateChanged;
        
        public void Enable() => Enable(ScriptableObject.CreateInstance<UI_Menu_Transition_Instant>());

        public void Disable() => Disable(ScriptableObject.CreateInstance<UI_Menu_Transition_Instant>());

        public void Toggle() => Toggle(ScriptableObject.CreateInstance<UI_Menu_Transition_Instant>());

        public void Enable(UI_Menu_Transition transition)
        {
            if (!UI_Menu_Manager.TryGet(out UI_Menu_Manager result, false)) return;
            
            result.SimpleStateChange(GetMenu(), MenuStateCompact.ENABLED, transition);
        }

        public void Disable(UI_Menu_Transition transition)
        {
            if (!UI_Menu_Manager.TryGet(out UI_Menu_Manager result, false)) return;
            
            result.SimpleStateChange(GetMenu(), MenuStateCompact.DISABLED, transition);
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
        
        public void Transition(int i)
        {
            Transition(transitions[i].Menu, transitions[i].Transition);
        }

        public void Transition(UI_Menu menuOut, UI_Menu_Transition transition, bool allowBack = true)
        {
            if (UI_Menu_Manager.TryGet(out UI_Menu_Manager result, false))
            {
                result.Transition(GetMenu(), menuOut, transition, allowBack);
            }
        }

        public void SetState(MenuState newState)
        {
            state = newState;

            switch (GetCompactState())
            {
                case MenuStateCompact.ENABLED:

                    if (Application.isPlaying)
                    {
                        OnMenuEnabled();
                    }

                    break;
                case MenuStateCompact.DISABLED:

                    if (Application.isPlaying)
                    {
                        GetMenu().OnMenuDisabled();
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

            ScriptableObject.CreateInstance<UI_Menu_Transition_Instant>()
                .Transition2(menu, menuEnabled ? menu : null, menuEnabled ? null : menu);
        }
    }
}