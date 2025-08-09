using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IbrahKit
{
    [DefaultExecutionOrder(Execution_Order.ui)]
    public class UI_Menu_Manager : Manager_Base<UI_Menu_Manager>
    {
        public const string UILAYOUTKEY = "UILayouts";

        [SerializeField, Dropdown(UILAYOUTKEY)] private List<string> activeLayouts;

        [SerializeField] private List<UI_Menu_Basic> activeMenus = new();

        public Action<UI_Menu_Basic, StateMode> OnCustomTR;

        private void OnDisable()
        {
            if (Instance != this) return;
        }

        public bool ShowLayout(List<string> layouts)
        {
            return activeLayouts.Intersect(layouts).Count() > 0;
        }

        public void Transition(UI_Menu_Basic menuIn, UI_Menu_Basic menuOut, FadeMode fadeMode, float _fadeTime)
        {
            StartCoroutine(TransitionRoutine(menuIn, menuOut, fadeMode, _fadeTime));
        }

        public IEnumerator TransitionRoutine(UI_Menu_Basic menuIn, UI_Menu_Basic menuOut, FadeMode fadeMode, float _fadeTime)
        {
            yield return StartCoroutine(FadeRoutine(menuIn, StateMode.Disable, fadeMode, _fadeTime));

            yield return StartCoroutine(FadeRoutine(menuOut, StateMode.Enable, fadeMode, _fadeTime));

            menuOut.SetPreviousMenu(menuIn);
        }

        public void Fade(UI_Menu_Basic menu, StateMode stateMode, FadeMode fadeMode, float _fadeTime)
        {
            StartCoroutine(FadeRoutine(menu, stateMode, fadeMode, _fadeTime));
        }

        public IEnumerator FadeRoutine(UI_Menu_Basic menu, StateMode stateMode, FadeMode fadeMode, float _fadeTime)
        {
            switch (stateMode)
            {
                case StateMode.Enable:

                    menu.SetActive(true);

                    switch (fadeMode)
                    {
                        case FadeMode.None:

                            menu.SetAlpha(1);

                            menu.SetInteractable(true);

                            break;
                        case FadeMode.Time:

                            while (menu.GetAlpha() < 1)
                            {
                                menu.SetAlpha(menu.GetAlpha() + Time.deltaTime / _fadeTime);
                                yield return null;
                            }

                            menu.SetInteractable(true);

                            break;
                        case FadeMode.Custom:

                            OnCustomTR?.Invoke(menu, stateMode);

                            break;
                    }

                    AddMenu(menu);

                    break;
                case StateMode.Disable:

                    switch (fadeMode)
                    {
                        case FadeMode.None:

                            menu.SetAlpha(0);

                            menu.SetInteractable(false);

                            menu.SetActive(false);

                            break;
                        case FadeMode.Time:

                            menu.SetInteractable(false);

                            while (menu.GetAlpha() > 0)
                            {
                                menu.SetAlpha(menu.GetAlpha() - Time.deltaTime / _fadeTime);
                                yield return null;
                            }

                            menu.SetActive(false);

                            break;
                        case FadeMode.Custom:

                            OnCustomTR?.Invoke(menu, stateMode);

                            break;
                    }

                    RemoveMenu(menu);

                    break;
            }
        }

        public void AddMenu(UI_Menu_Basic menu)
        {
            activeMenus.Add(menu);
        }

        public void RemoveMenu(UI_Menu_Basic menu)
        {
            activeMenus.Remove(menu);
        }
    }

    public enum FadeMode
    {
        None,
        Time,
        Custom,
    }

    public enum StateMode
    {
        Enable,
        Disable,
    }

    public enum InputType
    {
        KEYBOARD,
        MOUSE,
        GAMEPAD,
        TOUCHSCREEN,
    }
}