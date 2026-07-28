#region

using System;
using System.Collections.Generic;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public class UI_Menu_Controller_Content : UI_Menu_Controller, IMenuControllerContent
    {
        private State state = State.BEFOREINIT;

        [SerializeField, Required] private UI_Menu_Controller_Canvas canvasController;

        private readonly List<GameObject> spawnedMenuItems = new();

        private readonly Queue<IMenuInit> uninitialized = new();

        protected override void OnInit()
        {
            AfterMenuItems();

            List<IMenuInit> subtree =
                Transform_Utilities.GetComponentsByLevel<IMenuInit>(GetMenu().transform, true, true);

            InitializeSubTree(subtree);

            state = State.AFTERINIT;
        }

        public virtual void AfterMenuItems()
        {
        }

        public override void OnMenuEnabled()
        {
        }

        public override void Lifecycle()
        {
            if (state != State.AFTERINIT) return;

            while (uninitialized.Count > 0)
            {
                IMenuInit up = uninitialized.Dequeue();

                List<IMenuInit> subtree = up.transform.GetComponentsByLevel<IMenuInit>(true, true);

                InitializeSubTree(subtree);
            }
        }

        public override void OnMenuDisabled()
        {
        }





        public void RegisterUI(IMenuInit element)
        {
            if (state == State.BEFOREINIT) return;

            uninitialized.Enqueue(element);
        }

        private void InitializeSubTree(List<IMenuInit> elements)
        {
            foreach (var item in elements)
            {
                item.OnMenuInit(GetMenu());
            }
        }

        public UI_Menu_Config GetMenuConfig()
        {
            UI_Configs.TryGet<UI_Menu_Config_Override, UI_Menu_Config_SO, UI_Menu_Config>(
                UI_Configs.GetConfigs(GetMenu().transform), out UI_Menu_Config result);

            return result;
        }

        public UI_Menu_Controller_Canvas GetCanvasController() => canvasController;

        private enum State
        {
            BEFOREINIT,
            AFTERINIT,
        }
    }
}