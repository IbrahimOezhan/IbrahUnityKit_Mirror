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

        private int openCounter;

        private UI_Menu menu;

        [TabGroup("Menu Items", order: -1), Tooltip("Parent transform for list menu items.")]
        [SerializeField]
        private Transform list;

        [TabGroup("Menu Items", order: -1), Tooltip("List of predefined menu items."), SerializeReference, ShowIf(nameof(ShowMenuItems))]
        private List<Menu_Item_Base> listMenuItems = new();

        [TabGroup("Menu Settings", order: -1), Tooltip("If true, reload menu items every time the menu is opened.")]
        [SerializeField]
        private bool reloadOnOpen;

        [TabGroup("Runtime", order: -1), ShowInInspector, ReadOnly]
        protected List<GameObject> spawnedMenuItems = new();

        [TabGroup("Runtime", order: -1), SerializeField, ReadOnly]
        protected List<IMenuUpdate> menuUI = new();

        public void Init(UI_Menu menu)
        {
            this.menu = menu;
        }

        private void ClearMenuContent()
        {
            foreach (var item in spawnedMenuItems)
            {
                Object.Destroy(item);
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
            SpawnMenuContent();

            MenuUpdate();

            menu.SendMessage(SENDMESSAGE, null, SendMessageOptions.DontRequireReceiver);

            yield return null;

            //UI_Navigation_Manager.Instance.UpdateSelectables();
        }

        private void SpawnMenuContent()
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

        public UI_Menu_Config GetMenuConfig()
        {
            UI_Configs.TryGet<UI_Menu_Config_Override, UI_Menu_Config_SO, UI_Menu_Config>(UI_Configs.GetConfigs(menu.transform), out UI_Menu_Config result);

            return result;
        }

        private bool ShowMenuItems() => list != null;

        public void Awake()
        {

        }

        public void Enable()
        {
            if (reloadOnOpen || (!reloadOnOpen && openCounter++ == 0)) ReloadMenuContent();

            foreach (IMenuUpdate child in menuUI)
            {
                child.OnMenuEnabled();
            }
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