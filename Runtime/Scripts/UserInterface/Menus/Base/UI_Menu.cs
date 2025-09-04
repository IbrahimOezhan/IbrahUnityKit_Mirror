using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace IbrahKit
{
    public class UI_Menu : MonoBehaviour
    {
        private bool initialized = false;

        [TabGroup("Menu Items", order: -1), Tooltip("Custom menu configuration, optional."), SerializeField]
        private UI_Audio_SO overrideAudio;

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

        private void Init()
        {
            if (initialized) return;
            initialized = true;

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
            if (UI_Config_Manager.TryGet(out UI_Config_Manager result))
            {
                result.GetAudioConfig(overrideAudio).OnClick();
            }
        }

        public void OnHover()
        {
            if (UI_Config_Manager.TryGet(out UI_Config_Manager result))
            {
                result.GetAudioConfig(overrideAudio).OnHover();
            }
        }
    }
}