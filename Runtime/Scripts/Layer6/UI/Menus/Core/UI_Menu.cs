#region

using System;
using System.Collections.Generic;
using IbrahKit.UI.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    public class UI_Menu : MonoBehaviour, IConfigHolder, IMenuReference
    {
        [SerializeField] private UI_Configs configs;

        [SerializeField] private UI_Menu_Controller_Visibility visibility;

        [SerializeField] private UI_Menu_Controller_State state;

        private readonly UI_Menu_Controller_Audio audioController = new();
        
        private readonly List<UI_Menu_Controller> controllers = new();

        public Action<bool> OnStateChanged;

        protected virtual void Awake()
        {
            controllers.Add(visibility);

            controllers.Add(state);

            controllers.Add(audioController);

            BeforeInit();
            
            UI_Init.InitSubTree(transform);

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

        public UI_Configs GetConfigs() => configs;

        public UI_Menu GetMenu() => this;
        
        public UI_Menu_Controller_Audio GetAudioController() => audioController;

        public List<UI_Menu_Controller> GetMenuControllers() => controllers;

        public IMenuControllerVisibility GetVisbilityController() => visibility;

        public IMenuControllerState GetStateController() => state;

        [Button("Toggle")]
        public void ToggleEditor()
        {
            state.ToggleEditor(this);
        }
    }
}