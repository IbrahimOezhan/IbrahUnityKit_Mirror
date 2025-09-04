using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class Menu_Content_Controller : IUnityCallbacks, IMenuContent
    {
        private const string SENDMESSAGE = "OnMenuLoaded";

        private UI_Menu menu;

        [TabGroup("Menu Items", order: -1), Tooltip("Parent transform for list menu items.")]
        [SerializeField]
        private Transform list;

        [TabGroup("Menu Items", order: -1), Tooltip("Custom menu configuration, optional."), SerializeField]
        private UI_Menu_Config_SO overrideMenuConfig;

        [TabGroup("Menu Items", order: -1), Tooltip("List of predefined menu items."), SerializeField, ShowIf(nameof(ShowMenuItems))]
        private List<Menu_Item> listMenuItems = new();

        [TabGroup("Menu Settings", order: -1), Tooltip("If true, reload menu items every time the menu is opened.")]
        [SerializeField]
        private bool reloadOnOpen;

        [TabGroup("Runtime", order: -1), ShowInInspector, ReadOnly]
        protected List<GameObject> spawnedMenuItems = new();

        [TabGroup("Runtime", order: -1), SerializeField, ReadOnly]
        protected List<IMenuUpdate> menuUI = new();

        private bool ShowMenuItems()
        {
            return list != null;
        }

        public void Init(UI_Menu menu)
        {
            this.menu = menu;
        }

        private void ClearMenuContent()
        {
            foreach (var item in spawnedMenuItems)
            {
                GameObject.Destroy(item);
            }

            spawnedMenuItems.Clear();
        }

        public void ReloadMenuContent()
        {
            ClearMenuContent();

            LoadMenuContent();
        }

        private void LoadMenuContent()
        {
            menu.StartCoroutine(LoadMenuContentRoutine());
        }

        private IEnumerator LoadMenuContentRoutine()
        {
            List<Setting> _settings = new();

            SpawnMenuContent(_settings);

            MenuUpdate();

            menu.SendMessage(SENDMESSAGE, null, SendMessageOptions.DontRequireReceiver);

            yield return null;

            //UI_Navigation_Manager.Instance.UpdateSelectables();
        }

        private void SpawnMenuContent(List<Setting> _settings)
        {
            foreach (Menu_Item menuItem in listMenuItems)
            {
                if (SpawnMenuItem(menuItem, list as RectTransform, out GameObject _instance))
                {
                    spawnedMenuItems.Add(_instance);
                }
            }
        }

        public bool SpawnMenuItem(Menu_Item menuItem, RectTransform parent, out GameObject _goInstance)
        {
            _goInstance = null;

            _goInstance = menuItem.Spawn(parent, menu);

            return _goInstance != null;
        }

        public void AddUI(IMenuUpdate menuUpdate)
        {
            menuUI.Add(menuUpdate);

            MenuUpdate();
        }

        public void RemoveUI(IMenuUpdate menuUpdate)
        {
            menuUI.Remove(menuUpdate);

            MenuUpdate();
        }

        public void MenuUpdate()
        {
            foreach (IMenuUpdate child in menuUI)
            {
                child.MenuUpdate();
            }
        }

        public UI_Menu_Config_SO GetMenuConfig()
        {
            if (UI_Config_Manager.TryGet(out UI_Config_Manager result))
            {
                return result.GetMenuConfig(overrideMenuConfig);
            }

            return overrideMenuConfig;
        }

        public void Awake()
        {
            if (!reloadOnOpen) ReloadMenuContent();
        }

        public void Enable()
        {
            if (reloadOnOpen) ReloadMenuContent();
        }

        public void Start()
        {

        }

        public void Disable()
        {

        }

        public void Destroy()
        {

        }
    }
}