using UnityEngine;

namespace IbrahKit
{
    public abstract class UI_Localization : UI_Extension
    {
        [Dropdown("Localization"), SerializeField]
        protected string key;

        [SerializeField]
        protected string fallbackText;

        [SerializeField]
        protected string[] parameters;

        protected override void Awake()
        {
            base.Awake();

            if (Local_Manager.Instance != null) Local_Manager.Instance.OnLanguageChanged += UpdateUI;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (Local_Manager.Instance != null) Local_Manager.Instance.OnLanguageChanged -= UpdateUI;
        }

        public void SetFallback(string _fallback)
        {
            if (fallbackText == _fallback) return;

            fallbackText = _fallback;

            UpdateUI();
        }

        public void SetKey(string _key)
        {
            if (key == _key) return;

            key = _key;

            UpdateUI();
        }

        public void SetParam(params string[] _params)
        {
            parameters = _params;

            UpdateUI();
        }

        public void SetKeyParam(string _key, params string[] _params)
        {
            key = _key;

            parameters = _params;

            UpdateUI();
        }

        protected string GetContent(Local_Manager manager)
        {
            return manager.GetString(key, fallbackText, parameters);
        }
    }
}