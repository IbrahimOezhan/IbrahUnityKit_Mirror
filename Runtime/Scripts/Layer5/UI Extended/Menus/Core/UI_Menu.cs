#region

using System;
using IbrahKit.UI.Core.Config;
using IbrahKit.UI.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    public partial class UI_Menu : MonoBehaviour, IUIConfigHolder, IMenuReference
    {
        [SerializeField] private UI_Configs configs;

        protected virtual void Awake()
        {
            BeforeInit();

            UI_Init.InitSubTree(transform);

            AfterInit();
        }

        private void Update()
        {
            ObjectLifecycle();

            if (GetState() == MenuState.ENABLED)
            {
                MenuLifecycle();
            }
        }

        public UI_Menu GetMenu() => this;

        public bool TryGetConfig<TConfig>(out TConfig config) where TConfig : UI_Config<TConfig>
        {
            return configs.TryGet(out config);
        }

        [Button("Toggle")]
        public void ToggleEditor()
        {
            ToggleEditor(this);
        }

        [Serializable]
        private class MenuTransition
        {
            [SerializeField] private UI_Menu_Transition transition;
            [SerializeField] private UI_Menu to;

            public UI_Menu Menu => to;
            public UI_Menu_Transition Transition => transition;
        }
    }
}