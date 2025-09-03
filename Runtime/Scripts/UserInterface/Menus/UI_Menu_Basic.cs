using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace IbrahKit
{
    public class UI_Menu_Basic : MonoBehaviour
    {
        private const string SENDMESSAGE = "OnMenuLoaded";

        [TabGroup("Menu Settings",order: -1), Tooltip("If true, reload menu items every time the menu is opened.")]
        [SerializeField]
        private bool reloadOnOpen;

        [TabGroup("Menu Items", order: -1), Tooltip("Parent transform for list menu items.")]
        [SerializeField]
        private Transform list;

        [TabGroup("Menu Items", order: -1), Tooltip("Custom menu configuration, optional."), SerializeField]
        private UI_Menu_Config_SO customConfig;

        [TabGroup("Menu Items", order: -1), Tooltip("Custom menu configuration, optional."), SerializeField]
        private UI_Audio_SO overrideAudio;

        [TabGroup("Menu Items", order: -1), Tooltip("List of predefined menu items."),SerializeField, ShowIf("@list != null")]
        private List<Menu_Item> listMenuItems = new();

        [TabGroup("Runtime", order: -1), ShowInInspector, ReadOnly]
        protected List<GameObject> spawnedMenuItems = new();

        [TabGroup("Runtime", order: -1), ShowInInspector, ReadOnly]
        protected List<GameObject> spawnedListMenuItems = new();

        [TabGroup("Runtime", order: -1), SerializeField, ReadOnly]
        private HashSet<string> hiddenBy = new();

        [TabGroup("Runtime", order: -1), SerializeField, ReadOnly]
        protected UI_Menu_Basic previousMenu;

        [TabGroup("Runtime", order: -1), SerializeField, ReadOnly]
        protected List<IMenuUpdate> menuUI = new();

        [TabGroup("Menu Settings", order: -1), SerializeField, Tooltip("Whether menu should hide automatically on pause")]
        protected bool preventHideOnPause;

        [TabGroup("Menu Settings", order: -1), SerializeField, Tooltip("Disable menu on start")]
        protected bool disableOnStart;

        [TabGroup("Menu Settings", order: -1), SerializeField, Tooltip("CanvasGroup controlling menu visibility and interactivity")]
        protected CanvasGroup enabledGroup;

        [TabGroup("Menu Settings", order: -1), SerializeField, Tooltip("CanvasGroup used when menu is hidden")]
        protected CanvasGroup hiddenGroup;

        [TabGroup("Transitions", order: -1), SerializeField, Tooltip("Menu to switch to when back action is triggered")]
        protected UI_Menu_Basic overrideBackMenu;

        [TabGroup("Transitions", order: -1), SerializeField, Tooltip("Available transitions from this menu")]
        private List<UI_Menu_Transition> transitions;

        public Action<bool> OnStateChanged;

        protected virtual void Awake()
        {
            MenuUpdate();

            if (!reloadOnOpen) LoadMenuItems();
        }

        protected virtual void Start()
        {
            if (IsEnabled())
            {
                UI_Menu_Manager.Instance.AddMenu(this);
            }

            if (disableOnStart)
            {
                Disable();
            }
        }

        protected virtual void OnEnable()
        {
            if (!preventHideOnPause && Pause_Manager.Instance != null)
            {
                Pause_Manager.Instance.OnPause += OnPause;
                Pause_Manager.Instance.UpdatePause();
            }

            if (Game_Utilities.Instance != null)
            {
                Game_Utilities.Instance.OnHide += GU_Hide;
                Game_Utilities.Instance.UpdateHide();
            }

            if (reloadOnOpen) ReloadMenu();
        }

        protected virtual void OnDisable()
        {
            if (!preventHideOnPause && Pause_Manager.Instance != null)
            {
                Pause_Manager.Instance.OnPause -= OnPause;
            }

            if (Game_Utilities.Instance != null)
            {
                Game_Utilities.Instance.OnHide -= GU_Hide;
            }
        }

        protected virtual void OnDestroy()
        {

        }

        private void OnRectTransformDimensionsChange()
        {
            MenuUpdate();
        }

        private void OnApplicationFocus(bool _focus)
        {
            MenuUpdate();
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

        protected void MenuUpdate()
        {
            foreach (IMenuUpdate child in menuUI)
            {
                child.MenuUpdate();
            }
        }

        public void SetAlpha(float alpha)
        {
            enabledGroup.alpha = alpha;
        }

        public void SetInteractable(bool val)
        {
            enabledGroup.interactable = val;
        }

        public void SetPreviousMenu(UI_Menu_Basic menu)
        {
            previousMenu = menu;
        }

        public void SetActive(bool val)
        {
            gameObject.SetActive(val);

            OnStateChanged?.Invoke(val);
        }

        public void Toggle()
        {
            if (IsEnabled())
            {
                Disable();
            }
            else
            {
                Enable();
            }
        }

        [BoxGroup("Buttons", order: -3), Button]
        public void Enable()
        {
            Enable(null);
        }

        [BoxGroup("Buttons", order: -3), Button]
        public void Disable()
        {
            Disable(FadeMode.None, 0);
        }

        public void Enable(UI_Menu_Basic _enabledFrom, FadeMode fadeMode = FadeMode.None, float _fadeTime = 0)
        {
            if (UI_Menu_Manager.Instance != null)
            {
                SetPreviousMenu(_enabledFrom);
                UI_Menu_Manager.Instance.Fade(this, StateMode.Enable, fadeMode, _fadeTime);
            }
            else
            {
                SetActive(true);
                enabledGroup.alpha = 1;
                enabledGroup.interactable = true;
            }
        }

        public void Disable(FadeMode fadeMode = FadeMode.None, float _fadeTime = 0)
        {
            if (UI_Menu_Manager.Instance != null)
            {
                UI_Menu_Manager.Instance.Fade(this, StateMode.Disable, fadeMode, _fadeTime);
            }
            else
            {
                SetActive(false);
                enabledGroup.alpha = 0;
                enabledGroup.interactable = false;
            }
        }

        public void MenuTransition(UI_Menu_Basic _menu)
        {
            MenuTransition(_menu, null);
        }

        public void MenuTransition(UI_Menu_Basic _menu, UI_Menu_Basic _overrideBackMenu = null)
        {
            if (_overrideBackMenu != null)
            {
                _menu.overrideBackMenu = _overrideBackMenu;
            }

            UI_Menu_Manager.Instance.Transition(this, _menu, FadeMode.None, 0);
        }

        public void MenuTransition(int _index)
        {
            UI_Menu_Manager.Instance.Transition(this, transitions[_index]);
        }

        public void MenuTransitionToPrevious()
        {
            MenuTransition(previousMenu);
        }

        private void GU_Hide(bool state)
        {
            if (state) Hide("Debug");
            else Show("Debug");
        }

        private void OnPause(bool state)
        {
            if (state) Hide("paused");
            else Show("paused");
        }

        public void Hide(string hide)
        {
            if (hiddenBy.Add(hide))
            {
                hiddenGroup.alpha = 0;
            }
        }

        public void Show(string show)
        {
            if (hiddenBy.Remove(show))
            {
                if (hiddenBy.Count == 0)
                {
                    hiddenGroup.alpha = 1;
                }
            }
        }

        public void ReloadMenu()
        {
            ClearMenuItems();

            LoadMenuItems();
        }

        private void ClearMenuItems()
        {
            foreach (var item in spawnedMenuItems)
            {
                Destroy(item);
            }

            spawnedMenuItems.Clear();

            spawnedListMenuItems.Clear();
        }

        private void LoadMenuItems()
        {
            StartCoroutine(LoadMenuItemsRoutine());
        }

        private IEnumerator LoadMenuItemsRoutine()
        {
            List<Setting> _settings = new();

            SpawnListItems(_settings);

            MenuUpdate();

            SendMessage(SENDMESSAGE, null, SendMessageOptions.DontRequireReceiver);

            yield return null;

            //UI_Navigation_Manager.Instance.UpdateSelectables();
        }

        private void SpawnListItems(List<Setting> _settings)
        {
            foreach (Menu_Item menuItem in listMenuItems)
            {
                if (SpawnMenuItem(menuItem, list as RectTransform, out GameObject _instance))
                {
                    spawnedListMenuItems.Add(_instance);
                    spawnedMenuItems.Add(_instance);
                }
            }
        }

        public void OnClick()
        {
            if (UI_Config_Manager.TryGet(out UI_Config_Manager result))
            {
                result.GetAudioConfig(overrideAudio).OnClick();
            }
        }

        public void OnHover()
        {
            if (UI_Config_Manager.TryGet(out UI_Config_Manager result))
            {
                result.GetAudioConfig(overrideAudio).OnHover();
            }
        }

        public virtual void Back()
        {
            if (overrideBackMenu != null)
            {
                MenuTransition(overrideBackMenu);
                overrideBackMenu = null;
            }
            else MenuTransitionToPrevious();
        }

        public UI_Menu_Config_SO GetMenuConfig()
        {
            if (UI_Config_Manager.TryGet(out UI_Config_Manager result))
            {
                return result.GetMenuConfig(customConfig);
            }

            return customConfig;
        }

        public float GetAlpha()
        {
            return enabledGroup.alpha;
        }

        public bool SpawnMenuItem(Menu_Item menuItem, RectTransform parent, out GameObject _goInstance)
        {
            _goInstance = null;

            _goInstance = menuItem.Spawn(parent, this);

            return _goInstance != null;
        }

        public bool IsEnabled()
        {
            return enabledGroup.alpha == 1;
        }

        public bool IsDisabled()
        {
            return enabledGroup.alpha == 0;
        }
    }
}