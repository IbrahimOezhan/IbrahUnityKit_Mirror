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

        private readonly UI_Menu_Controller_Audio audioController = new();

        [SerializeField]
        private UI_Configs configs;

        [SerializeField]
        private UI_Menu_Controller_Content content;

        [SerializeField]
        private UI_Menu_Controller_Visibility visiblity;

        [SerializeField]
        private UI_Menu_Controller_State state;

        public Action<bool> OnStateChanged;

        protected virtual void Awake() { }

        protected virtual void OnEnable() { }

        private void Update()
        {
            controllers.ForEach(x => x.Lifecycle());

            MenuLifecycle();
        }

        protected virtual void OnDisable() { }

        public virtual void OnMenuEnabled()
        {
            if (firstOpen)
            {
                controllers.Add(content);

                controllers.Add(visiblity);

                controllers.Add(state);

                controllers.Add(audioController);

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

        public UI_Menu_Controller_Audio GetAudioController() => audioController;

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