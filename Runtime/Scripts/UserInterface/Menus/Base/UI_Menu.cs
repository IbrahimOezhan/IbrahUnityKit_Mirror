using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace IbrahKit
{
    public class UI_Menu : MonoBehaviour, IConfig
    {
        private bool initialized = false;

        [SerializeField]
        private UI_Configs configs;

        [SerializeField] private Menu_Content_Controller content;
        [SerializeField] private Menu_Visibility_Controller visiblity;
        [SerializeField] private Menu_State_Controller state;

        public Action<bool> OnStateChanged;

        protected virtual void Awake()
        {
            Init();

            content.Awake();
            visiblity.Awake();
        }

        protected virtual void OnEnable()
        {
            content.Enable();
            visiblity.Enable();
        }

        protected virtual void OnDisable()
        {
            content.Disable();
            visiblity.Disable();
        }

        protected virtual void OnDestroy()
        {
            content.Destroy();
            visiblity.Destroy();
        }

        private void OnRectTransformDimensionsChange()
        {
            content.MenuUpdate();
        }

        private void OnApplicationFocus(bool _focus)
        {
            content.MenuUpdate();
        }

        [Button]
        private void Enable()
        {
            GetStateController().Enable();
        }

        [Button]
        private void Disable()
        {
            GetStateController().Disable();
        }

        private void Init()
        {
            if (initialized) return;
            if (Application.isPlaying) initialized = true;

            visiblity.Init(this);
            content.Init(this);
            state.Init(this);
        }

        public IMenuVisibility GetVisbilityController()
        {
            Init();
            return visiblity;
        }

        public IMenuState GetStateController()
        {
            Init();
            return state;
        }

        public IMenuContent GetContentController()
        {
            Init();
            return content;
        }

        public void OnClick()
        {
            if (UI_Configs.GetAudio(UI_Configs.GetConfigs(transform),out UI_Audio_Config_SO result))
            {
                result.GetConfig().OnClick();
            }
        }

        public void OnHover()
        {
            if (UI_Configs.GetAudio(UI_Configs.GetConfigs(transform), out UI_Audio_Config_SO result))
            {
                result.GetConfig().OnHover();
            }
        }

        public UI_Configs GetConfigs()
        {
            return configs;
        }
    }
}