using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class UI_Menu_Controller_Content : UI_Menu_Controller, IMenuContent
    {
        private const string SENDMESSAGE = "OnMenuLoaded";

        private UI_Menu menu;

        private State state = State.BEFOREINIT;

        [TabGroup("Menu Items", order: -1), Tooltip("Parent transform for list menu items.")]
        [SerializeField]
        private Transform list;

        [TabGroup("Menu Items", order: -1), Tooltip("List of predefined menu items."), SerializeReference, ShowIf(nameof(ShowMenuItems))]
        private List<Menu_Item_Base> listMenuItems = new();

        private readonly List<GameObject> spawnedMenuItems = new();

        private readonly Queue<IMenuUpdate> uninitialized = new();

        public override void Init(UI_Menu menu)
        {
            this.menu = menu;

            LoadMenuContent();

            List<IMenuUpdate> subtree = Transform_Utilities.GetComponentsByLevel<IMenuUpdate>(menu.transform, true, true);

            InitializeSubTree(subtree);

            state = State.AFTERINIT;
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
            if (!menuItem.TrySpawn(parent, menu, out result)) return false;

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
                item.OnMenuInit(menu);
            }
        }

        public UI_Menu_Config GetMenuConfig()
        {
            UI_Configs.TryGet<UI_Menu_Config_Override, UI_Menu_Config_SO, UI_Menu_Config>(UI_Configs.GetConfigs(menu.transform), out UI_Menu_Config result);

            return result;
        }

        private bool ShowMenuItems() => list != null;

        private enum State
        {
            BEFOREINIT,
            AFTERINIT,
        }
    }
}