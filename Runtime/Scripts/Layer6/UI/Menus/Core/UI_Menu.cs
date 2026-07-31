#region

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class UI_Menu : MonoBehaviour, IConfigHolder
    {
        [SerializeField] private UI_Configs configs;

        [SerializeField] private UI_Menu_Controller_Content content;

        [SerializeField] private UI_Menu_Controller_Visibility visiblity;

        [SerializeField] private UI_Menu_Controller_State state;

        private readonly UI_Menu_Controller_Audio audioController = new();
        private readonly List<UI_Menu_Controller> controllers = new();

        public Action<bool> OnStateChanged;

        protected virtual void Awake()
        {
            controllers.Add(content);

            controllers.Add(visiblity);

            controllers.Add(state);

            controllers.Add(audioController);

            BeforeInit();

            controllers.ForEach(x => x.Init(this));

            AfterInit();
        }

        private void Update()
        {
            ObjectLifecycle();

            if (state.GetState() == MenuState.ENABLED)
            {
                controllers.ForEach(x => x.Lifecycle());

                MenuLifecycle();
            }
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }

        protected virtual void OnDestroy()
        {
        }

        public UI_Configs GetConfigs() => configs;

        protected virtual void ObjectLifecycle()
        {
        }

        public virtual void OnMenuEnabled()
        {
        }

        protected virtual void BeforeInit()
        {
        }

        protected virtual void AfterInit()
        {
        }

        protected virtual void MenuLifecycle()
        {
        }

        public virtual void OnMenuDisabled()
        {
        }

        public UI_Menu_Controller_Audio GetAudioController() => audioController;

        public List<UI_Menu_Controller> GetMenuControllers() => controllers;

        public IMenuControllerVisibility GetVisbilityController() => visiblity;

        public IMenuControllerState GetStateController() => state;

        public IMenuControllerContent GetContentController() => content;

        [Button("Toggle")]
        public void ToggleEditor()
        {
            state.ToggleEditor(this);
        }
    }
}