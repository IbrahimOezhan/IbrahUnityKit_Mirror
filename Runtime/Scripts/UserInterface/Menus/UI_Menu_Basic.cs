using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace IbrahKit
{
    public class UI_Menu_Basic : MonoBehaviour
    {
        [TabGroup("Runtime Data"), SerializeField, ReadOnly]
        private HashSet<string> hiddenBy = new();
             
        [TabGroup("Runtime Data"), SerializeField, ReadOnly]
        protected InputType lastInputType;

        [TabGroup("Runtime Data"), SerializeField, ReadOnly]
        protected UI_Menu_Basic previousMenu;

        [TabGroup("Runtime Data"), SerializeField, ReadOnly]
        protected List<IMenuUpdate> menuUI = new();


        [TabGroup("Menu Settings", order: 0), SerializeField, Tooltip("CanvasGroup controlling menu visibility and interactivity")]
        protected CanvasGroup enabledGroup;

        [TabGroup("Menu Settings", order: 0), SerializeField, Tooltip("CanvasGroup used when menu is hidden")]
        protected CanvasGroup hiddenGroup;

        [TabGroup("Menu Settings", order: 0), SerializeField, Tooltip("Whether menu should hide automatically on pause")]
        protected bool preventHideOnPause;

        [TabGroup("Menu Settings", order: 0), SerializeField, Tooltip("Disable menu on start")]
        protected bool disableOnStart;


        [TabGroup("Transitions", order: 1), SerializeField, Tooltip("Menu to switch to when back action is triggered")]
        protected UI_Menu_Basic overrideBackMenu;

        [TabGroup("Transitions", order: 1), SerializeField, Tooltip("Available transitions from this menu")]
        private List<UI_Menu_Transition> transitions;


        public static Action<UI_Menu_Transition, UI_Menu_Basic> OnMenuTransition;

        protected virtual void Awake()
        {
            InitMenuContent();
        }

        protected virtual void Start()
        {
            if (IsEnabled())
            {
                UI_Manager.Instance.AddMenu(this);
            }

            if (disableOnStart)
            {
                Disable();
            }
        }

        protected virtual void OnEnable()
        {
            if (!preventHideOnPause)
            {
                Pause_Manager.Instance.OnPause += OnPause;
                Pause_Manager.Instance.UpdatePause();
            }

            if (Game_Utilities.Instance != null)
            {
                Game_Utilities.Instance.OnHide += GU_Hide;
                Game_Utilities.Instance.UpdateHide();
            }
        }

        protected virtual void OnDisable()
        {
            if (!preventHideOnPause)
            {
                Pause_Manager.Instance.OnPause -= OnPause;
            }

            if (Game_Utilities.Instance != null)
            {
                Game_Utilities.Instance.OnHide -= GU_Hide;
            }
        }

        protected virtual void OnDestroy()
        {

        }

        private void OnRectTransformDimensionsChange()
        {
            MenuUpdate();
        }

        private void OnApplicationFocus(bool _focus)
        {
            MenuUpdate();
        }

        protected void InitMenuContent()
        {
            menuUI = Transform_Utilities.GetComponentsInChildren<IMenuUpdate>(transform);

            MenuUpdate();
        }

        protected void MenuUpdate()
        {
            foreach (IMenuUpdate child in menuUI)
            {
                child.MenuUpdate(this);
            }
        }

        public void SetAlpha(float alpha)
        {
            enabledGroup.alpha = alpha;
        }

        public void SetInteractable(bool val)
        {
            enabledGroup.interactable = val;
        }

        public void SetPreviousMenu(UI_Menu_Basic menu)
        {
            previousMenu = menu;
        }

        public void SetActive(bool val)
        {
            gameObject.SetActive(val);

            if (val) OnMenuEnabled();
            else OnMenuDisable();
        }

        [BoxGroup("Buttons", order: -3), Button]
        public void Enable()
        {
            Enable(null);
        }

        [BoxGroup("Buttons", order: -3), Button]
        public void Disable()
        {
            Disable(FadeMode.None, 0);
        }

        public void Enable(UI_Menu_Basic _enabledFrom, FadeMode fadeMode = FadeMode.None, float _fadeTime = 0)
        {
            if (UI_Manager.Instance != null)
            {
                SetPreviousMenu(_enabledFrom);
                UI_Manager.Instance.Fade(this, StateMode.Enable, fadeMode, _fadeTime);
            }
            else
            {
                SetActive(true);
                enabledGroup.alpha = 1;
                enabledGroup.interactable = true;
            }
        }

        public void Disable(FadeMode fadeMode = FadeMode.None, float _fadeTime = 0)
        {
            if (UI_Manager.Instance != null)
            {
                UI_Manager.Instance.Fade(this, StateMode.Disable, fadeMode, _fadeTime);
            }
            else
            {
                SetActive(false);
                enabledGroup.alpha = 0;
                enabledGroup.interactable = false;
            }
        }

        protected virtual void OnMenuEnabled()
        {

        }

        protected virtual void OnMenuDisable()
        {

        }

        public void MenuTransition(UI_Menu_Basic _menu)
        {
            MenuTransition(_menu, null);
        }

        public void MenuTransition(UI_Menu_Basic _menu, UI_Menu_Basic _overrideBackMenu = null)
        {
            if (_overrideBackMenu != null)
            {
                _menu.overrideBackMenu = _overrideBackMenu;
            }

            UI_Manager.Instance.Transition(this, _menu, FadeMode.None, 0);
        }

        public void MenuTransition(int _index)
        {
            UI_Menu_Transition transition = transitions[_index];

            (UI_Menu_Basic menu, FadeMode mode, float time) = transition.GetData();

            UI_Manager.Instance.Transition(this, menu, mode, time);

            OnMenuTransition?.Invoke(transition, this);
        }

        public void MenuTransitionToPrevious()
        {
            MenuTransition(previousMenu);
        }

        private void GU_Hide(bool state)
        {
            if(state) Hide("Debug");
            else Show("Debug");
        }

        private void OnPause(bool state)
        {
            if (state) Hide("paused");
            else Show("paused");
        }

        public void Hide(string hide)
        {
            if (hiddenBy.Add(hide))
            {
                hiddenGroup.alpha = 0;
            }
        }

        public void Show(string show)
        {
            if(hiddenBy.Remove(show))
            {
                if(hiddenBy.Count == 0)
                {
                    hiddenGroup.alpha = 1;
                }
            }
        }

        public void SetParams(CanvasGroup enabled, CanvasGroup hidden)
        {
            enabledGroup = enabled;

            hiddenGroup = hidden;
        }

        public float GetAlpha()
        {
            return enabledGroup.alpha;
        }

        public bool IsEnabled()
        {
            return enabledGroup.alpha == 1;
        }

        public bool IsDisabled()
        {
            return enabledGroup.alpha == 0;
        }
    }
}