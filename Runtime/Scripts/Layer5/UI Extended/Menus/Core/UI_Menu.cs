#region

using System;
using System.Collections.Generic;
using IbrahKit.UI.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    public partial class UI_Menu : MonoBehaviour, IConfigHolder, IMenuReference
    {
        [SerializeField] private Configs configs;

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

        public bool TryGetConfig<TConfig>(out TConfig config) where TConfig : Config<TConfig>
        {
            return configs.TryGet(out config);
        }

        public UI_Menu GetMenu() => this;

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