using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using IbrahKit.UI;
using UnityEngine;

namespace IbrahKit
{
    public class UI_Menu : MonoBehaviour, IConfig
    {
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

        protected virtual void Awake()
        {
            controllers.Add(content);
            controllers.Add(visiblity);
            controllers.Add(state);

            BeforeInit();

            controllers.ForEach(x => x.Init(this));
        }

        protected virtual void BeforeInit()
        {

        }

        protected virtual void OnEnable()
        {
            controllers.ForEach(x => x.OnMenuEnabled());
        }

        protected virtual void Update()
        {
            controllers.ForEach(x => x.Lifecycle());
        }

        protected virtual void OnDisable()
        {
            controllers.ForEach(x => x.OnMenuDisabled());
        }

        private void OnRectTransformDimensionsChange()
        {
            OnFocusOrResolutionChanged?.Invoke();
        }

        private void OnApplicationFocus(bool _focus)
        {
            OnFocusOrResolutionChanged?.Invoke();
        }

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

        public IMenuVisibility GetVisbilityController()
        {
            return visiblity;
        }

        public IMenuState GetStateController()
        {
            return state;
        }

        public IMenuContent GetContentController()
        {
            return content;
        }

        public UI_Configs GetConfigs() => configs;

        [Button("Toggle")]
        public void ToggleEditor()
        {
            state.ToggleEditor(this);
        }
    }
}