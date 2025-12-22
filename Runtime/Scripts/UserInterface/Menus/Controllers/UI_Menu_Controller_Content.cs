using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public class UI_Menu_Controller_Content : UI_Menu_Controller, IMenuControllerContent
    {
        private State state = State.BEFOREINIT;

        [TabGroup("Menu Items", order: -1), Tooltip("Parent transform for list menu items."), SerializeField]
        private Transform list;

        [TabGroup("Menu Items", order: -1), Tooltip("List of predefined menu items."), SerializeReference, ShowIf(nameof(ShowMenuItems))]
        private List<Menu_Item_Base> listMenuItems = new();

        [SerializeField, Required] private UI_Menu_Controller_Canvas canvasController;

        private readonly List<GameObject> spawnedMenuItems = new();

        private readonly Queue<IMenuUpdate> uninitialized = new();

        protected override void OnInit()
        {
            LoadMenuContent();

            AfterMenuItems();

            List<IMenuUpdate> subtree = Transform_Utilities.GetComponentsByLevel<IMenuUpdate>(GetMenu().transform, true, true);

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
                IMenuUpdate up = uninitialized.Dequeue();

                List<IMenuUpdate> subtree = Transform_Utilities.GetComponentsByLevel<IMenuUpdate>(up.transform, true, true);

                InitializeSubTree(subtree);
            }
        }

        public override void OnMenuDisabled()
        {

        }

        private void LoadMenuContent()
        {
            foreach (Menu_Item_Base menuItem in listMenuItems)
            {
                if (TrySpawnMenuItem(menuItem, list as RectTransform, out GameObject _instance))
                {
                    spawnedMenuItems.Add(_instance);
                }
            }
        }

        public bool TrySpawnMenuItem(Menu_Item_Base menuItem, RectTransform parent, out GameObject result)
        {
            if (!menuItem.TrySpawn(parent, GetMenu(), out result)) return false;

            return result != null;
        }

        public void RegisterUI(IMenuUpdate element)
        {
            if (state == State.BEFOREINIT) return;

            uninitialized.Enqueue(element);
        }

        private void InitializeSubTree(List<IMenuUpdate> elements)
        {
            foreach (var item in elements)
            {
                item.OnMenuInit(GetMenu());
            }
        }

        public UI_Menu_Config GetMenuConfig()
        {
            UI_Configs.TryGet<UI_Menu_Config_Override, UI_Menu_Config_SO, UI_Menu_Config>(UI_Configs.GetConfigs(GetMenu().transform), out UI_Menu_Config result);

            return result;
        }

        public UI_Menu_Controller_Canvas GetCanvasController() => canvasController;

        private bool ShowMenuItems() => list != null;

        private enum State
        {
            BEFOREINIT,
            AFTERINIT,
        }
    }
}