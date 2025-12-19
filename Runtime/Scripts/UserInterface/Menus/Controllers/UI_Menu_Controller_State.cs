using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public class UI_Menu_Controller_State : UI_Menu_Controller, IMenuControllerState
    {
        private UI_Menu menu;

        [SerializeField, ReadOnly] private State state = State.DISABLED;

        [SerializeField, ReadOnly] private UI_Menu previous;

        private static readonly HashSet<UI_Menu> activeMenus = new();

        public void ToggleEditor(UI_Menu menu)
        {
            bool menuEnabled = GetCompactState() == StateCompact.ENABLED;

            Menu_Transition.Transition(menuEnabled ? menu : null, menuEnabled ? null : menu);
        }

        public void Enable()
        {
            Enable<Menu_Transition_Instant>();
        }

        public void Enable<T>(params object[] args) where T : Menu_Transition
        {
            Menu_Transition tr = GenericToTransition<T>(null, menu, args);

            Transition(tr);
        }

        public void Disable()
        {
            Disable<Menu_Transition_Instant>();
        }

        public void Disable<T>(params object[] args) where T : Menu_Transition
        {
            Menu_Transition tr = GenericToTransition<T>(menu, null, args);

            Transition(tr);
        }

        public void Toggle()
        {
            Toggle<Menu_Transition_Instant>();
        }

        public void Toggle<T>(params object[] args) where T : Menu_Transition
        {
            bool menuEnabled = GetCompactState() == StateCompact.ENABLED;

            Menu_Transition tr = GenericToTransition<T>(menuEnabled ? menu : null, menuEnabled ? null : menu, args);

            Transition(tr);
        }

        public void Transition<T>(UI_Menu menuOut, UI_Menu backOverride = null, params object[] args) where T : Menu_Transition
        {
            Transition(GenericToTransition<T>(menu, menuOut, args), backOverride);
        }

        public void TransitionToPrevious<T>(UI_Menu backOverride = null, params object[] args) where T : Menu_Transition
        {
            Transition(GenericToTransition<T>(menu, previous, args), backOverride);
        }

        private void Transition(Menu_Transition tr, UI_Menu backOverride = null)
        {
            if (UI_Menu_Manager.TryGet(out UI_Menu_Manager result, false))
            {
                result.Transition(tr, backOverride);
            }
        }

        public void SetPreviousMenu(UI_Menu menu)
        {
            previous = menu;
        }

        public void SetState(State state)
        {
            this.state = state;

            switch (GetCompactState())
            {
                case StateCompact.ENABLED:

                    activeMenus.Add(menu);

                    if (Application.isPlaying)
                    {
                        menu.OnMenuEnabled();
                        menu.GetMenuControllers().ForEach(x => x.OnMenuEnabled());
                    }

                    break;
                case StateCompact.DISABLED:

                    activeMenus.Remove(menu);

                    if (Application.isPlaying)
                    {
                        menu.OnMenuDisabled();
                        menu.GetMenuControllers().ForEach(x => x.OnMenuDisabled());
                    }

                    break;
            }
        }

        public State GetState()
        {
            return state;
        }

        public StateCompact GetCompactState()
        {
            return state == State.ENABLED || state == State.ENABLING ? StateCompact.ENABLED : StateCompact.DISABLED;
        }

        private Menu_Transition GenericToTransition<T>(UI_Menu menuIn, UI_Menu menuOut, params object[] args)
        {
            object[] array = new object[args.Length + 2];

            array[0] = menuIn;

            array[1] = menuOut;

            for (int i = 2; i < array.Length; i++)
            {
                array[i] = args[i - 2];
            }

            return (Menu_Transition)Activator.CreateInstance(typeof(T), array);
        }

        public override void Init(UI_Menu menu)
        {
            this.menu = menu;
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

        public enum State
        {
            ENABLED = 0,
            ENABLING = 1,
            DISABLED = 2,
            DISABLING = 3,
        }

        public enum StateCompact
        {
            ENABLED,
            DISABLED,
        }
    }
}