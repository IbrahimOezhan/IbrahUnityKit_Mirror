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

        [SerializeField]
        private Menu_Content_Controller content;

        [SerializeField]
        private Menu_Visibility_Controller visiblity;

        [SerializeField]
        private Menu_State_Controller state;

        public Action<bool> OnStateChanged;

        protected virtual void Awake()
        {
            Init();

            content.Awake();
            visiblity.Awake();
        }

        protected virtual void OnEnable()
        {
            visiblity.Enable();
            content.Enable();
        }

        protected virtual void OnDisable()
        {
            visiblity.Disable();
            content.Disable();
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

        public UI_Configs GetConfigs() => configs;
    }
}