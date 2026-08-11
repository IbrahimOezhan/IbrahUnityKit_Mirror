#region

using System;
using System.Collections.Generic;
using IbrahKit.Core;
using IbrahKit.Debugging;
using IbrahKit.Input;
using IbrahKit.Manager;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [DefaultExecutionOrder(Execution_Order.ui)]
    public class UI_Menu_Manager : MonoBehaviourSingletonDontDestroyOnLoadData<UI_Menu_Manager, UI_Menu_Manager_Data>
    {
        private readonly Stack<UI_Menu> menuNavigationStack = new();

        public Action<bool> OnHide;

        private Action actionHide;

        private bool hidden;

        private void Start()
        {
            if (Input_Shortcut_Manager.TryGet(out Input_Shortcut_Manager res))
            {
                res.RegisterAction(GetManagerData().GetKey(), actionHide);
            }
        }

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            actionHide = Hide;
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            if (Input_Shortcut_Manager.TryGet(out Input_Shortcut_Manager res))
            {
                res.UnregisterAction(GetManagerData().GetKey(), actionHide);
            }
        }

        public void SimpleStateChange(UI_Menu menu, MenuStateCompact targetState, UI_Menu_Transition transition,
            bool countEnableToStack = true)
        {
            if (!menu)
            {
                IbrahDebug.LogWarning("Menu passed is null");

                return;
            }

            Debug.Log(0);


            switch (targetState)
            {
                case MenuStateCompact.DISABLED:
                    Debug.Log(1);
                    if (menuNavigationStack.Contains(menu) && menuNavigationStack.Peek() != menu)
                    {
                        IbrahDebug.LogError("Can only disable menus not in stack or the current menu in the stack");
                        return;
                    }

                    Debug.Log(2);

                    StartCoroutine(transition.MenuIn(menu));
                    break;
                case MenuStateCompact.ENABLED:
                    if (menuNavigationStack.Contains(menu))
                    {
                        IbrahDebug.LogError(
                            "Cannot enable menu that is already in stack. Use Transition to first disable the previous one");
                        return;
                    }

                    StartCoroutine(transition.MenuOut(menu));
                    if (menuNavigationStack.Count == 0 && countEnableToStack) menuNavigationStack.Push(menu);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetState), targetState, null);
            }
        }

        public void TransitionBack(UI_Menu_Transition transition)
        {
            Transition(menuNavigationStack.Peek(), menuNavigationStack.Pop(), transition);
        }

        public void Transition(UI_Menu from, UI_Menu to, UI_Menu_Transition transition, bool allowBack = true)
        {
            if (!transition)
            {
                IbrahDebug.LogWarning("Passed transition is null");
                return;
            }

            if (!from)
            {
                return;
            }

            if (!to)
            {
                return;
            }

            // If isn't allowed back -> Clear stack as to prevent gamepad back button to go back to a menu it shouldn't
            if (!allowBack)
            {
                menuNavigationStack.Clear();
                StartCoroutine(transition.Transition(this, from, to));
                menuNavigationStack.Push(to);
                return;
            }

            if (!menuNavigationStack.Contains(to))
            {
                menuNavigationStack.Push(to);
            }
            else
            {
                // If a loop was found for example: A > B > C > D > E  and now E transitions to C
                // pop all elements until reaching C so the stack becomes A > B > C and going back from C directs to B

                while (menuNavigationStack.Peek() != to)
                {
                    menuNavigationStack.Pop();
                }
            }

            StartCoroutine(transition.Transition(this, from, to));
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