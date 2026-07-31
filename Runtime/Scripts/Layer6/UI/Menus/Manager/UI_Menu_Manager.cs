#region

using System;
using System.Collections.Generic;
using IbrahKit.Core;
using IbrahKit.Debugging;
using IbrahKit.Input;
using IbrahKit.Manager;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [DefaultExecutionOrder(Execution_Order.ui)]
    public class UI_Menu_Manager : Manager_Global<UI_Menu_Manager, UI_Menu_Manager_Data>
    {
        private readonly Stack<UI_Menu> menuNavigationStack = new();

        public Action<bool> OnHide;

        private Action actionHide;

        private UI_Menu currentMenu = null;

        /// <summary>
        ///     TODO: PREVENT ENABLE DISABLE COUNTING TO NAV STACK. TO ACHIEVE THIS SAVE THE CURRENT MENU IN THE MANAGER
        /// </summary>
        private bool hidden;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            actionHide = Hide;

            if (Input_Shortcut_Manager.TryGet(out Input_Shortcut_Manager res))
            {
                res.RegisterAction(GetManagerData().GetKey(), actionHide);
            }
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            if (Input_Shortcut_Manager.TryGet(out Input_Shortcut_Manager res))
            {
                res.UnregisterAction(GetManagerData().GetKey(), actionHide);
            }
        }

        public void SimpleStateChange<T>(UI_Menu menu, MenuStateCompact targetState, params object[] args)
            where T : Menu_Transition
        {
            Debug.Log("Simple");

            if (menu == null)
            {
                IbrahDebug.LogWarning("Menu passed is null");

                return;
            }

            Menu_Transition transition = GenericToTransition<T>(
                targetState == MenuStateCompact.ENABLED ? null : menu,
                targetState == MenuStateCompact.ENABLED ? menu : null,
                args);
            Transition(transition);
        }

        public void Transition<T>(UI_Menu transitionTo, bool allowBack = true, params object[] args)
            where T : Menu_Transition
        {
            Transition(GenericToTransition<T>(currentMenu, transitionTo, args), allowBack);
        }

        public void TransitionBack<T>(params object[] args) where T : Menu_Transition
        {
            Transition(GenericToTransition<T>(null, menuNavigationStack.Pop(), args));
        }

        private void Transition(Menu_Transition transition, bool allowBack)
        {
            if (transition == null)
            {
                IbrahDebug.LogWarning("Passed transition is null");

                return;
            }

            currentMenu = transition.GetOut();

            // If isnt allowed back OR if destination is null -> Clear stack as to prevent gamepad back button to go back to a menu it shouldnt
            if (!allowBack || transition.GetOut() == null)
            {
                menuNavigationStack.Clear();
                Transition(transition);
                return;
            }

            if (transition.GetIn() != null)
            {
                if (!menuNavigationStack.Contains(transition.GetOut()))
                {
                    menuNavigationStack.Push(transition.GetIn());
                }
                else
                {
                    // If a loop was found for example: A > B > C > D > E > C so the menu skipped D when going to C
                    // pop all elements until reaching C and then pop C as well so the stack becomes A > B and going back from C directs to B

                    while (menuNavigationStack.Peek() != transition.GetOut())
                    {
                        menuNavigationStack.Pop();
                    }

                    menuNavigationStack.Pop();
                }
            }

            Transition(transition);
        }

        private void Transition(Menu_Transition transition)
        {
            StartCoroutine(transition.Transition(this));
        }

        private Menu_Transition GenericToTransition<T>(UI_Menu menuIn, UI_Menu menuOut, params object[] args)
            where T : Menu_Transition
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

        public void Hide()
        {
            hidden = !hidden;

            InvokeHide();
        }

        public void InvokeHide()
        {
            OnHide?.Invoke(hidden);
        }
    }
}