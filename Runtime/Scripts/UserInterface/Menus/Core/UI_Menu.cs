using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit.UI
{
    public class UI_Menu : MonoBehaviour, IConfigHolder
    {
        private bool firstOpen = true;

        private readonly List<UI_Menu_Controller> controllers = new();

        [SerializeField]
        private UI_Configs configs;

        [SerializeField]
        private UI_Menu_Controller_Content content;

        [SerializeField]
        private UI_Menu_Controller_Visibility visiblity;

        [SerializeField]
        private UI_Menu_Controller_State state;

        public Action OnFocusOrResolutionChanged;

        public Action<bool> OnStateChanged;

        protected virtual void Awake() { }

        protected virtual void OnEnable() { }

        private void Update()
        {
            controllers.ForEach(x => x.Lifecycle());

            MenuLifecycle();
        }

        protected virtual void OnDisable() { }

        private void OnRectTransformDimensionsChange()
        {
            OnFocusOrResolutionChanged?.Invoke();
        }

        private void OnApplicationFocus(bool _focus)
        {
            OnFocusOrResolutionChanged?.Invoke();
        }

        public virtual void OnMenuEnabled()
        {
            if (firstOpen)
            {
                controllers.Add(content);

                controllers.Add(visiblity);

                controllers.Add(state);

                BeforeInit();

                controllers.ForEach(x => x.Init(this));

                AfterInit();

                firstOpen = false;
            }
        }

        protected virtual void BeforeInit() { }

        protected virtual void AfterInit() { }

        protected virtual void MenuLifecycle() { }

        public virtual void OnMenuDisabled() { }

        public void OnClickAudio()
        {
            if (UI_Configs.TryGet<UI_Audio_Config_Override, UI_Audio_Config_SO, UI_Audio_Config>(UI_Configs.GetConfigs(transform), out UI_Audio_Config result))
            {
                result.OnClick();
            }
        }

        public void OnHoverAudio()
        {
            if (UI_Configs.TryGet<UI_Audio_Config_Override, UI_Audio_Config_SO, UI_Audio_Config>(UI_Configs.GetConfigs(transform), out UI_Audio_Config result))
            {
                result.OnHover();
            }
        }

        public List<UI_Menu_Controller> GetMenuControllers() => controllers;

        public IMenuControllerVisibility GetVisbilityController() => visiblity;

        public IMenuControllerState GetStateController() => state;

        public IMenuControllerContent GetContentController() => content;

        public UI_Configs GetConfigs() => configs;

        [Button("Toggle")]
        public void ToggleEditor()
        {
            state.ToggleEditor(this);
        }
    }
}