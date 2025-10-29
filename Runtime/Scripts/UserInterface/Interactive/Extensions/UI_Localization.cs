using UnityEngine;

namespace IbrahKit
{
    public class UI_Localization : UI_Text_Modifier
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

            if (Local_Manager.TryGet(out Local_Manager lm)) lm.OnLanguageChanged += UpdateUI;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (Local_Manager.TryGet(out Local_Manager lm)) lm.OnLanguageChanged -= UpdateUI;
        }

        public override void Execute()
        {
            if (!IsInitialized()) return;

            text.SetText(GetContent());
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

        protected string GetContent()
        {
            if (key.IsEmpty())
            {
                return "";
            }

            if (Local_Manager.TryGet(out Local_Manager result))
            {
                return result.GetString(key, fallbackText, parameters);
            }
            else
            {
#if UNITY_EDITOR
                Local_Config config = Local_Settings.Config();

                config.TryGetString(key, config.GetFirstLanguage(), out string res);

                return res;
#else
                return "Local Manager missing";
#endif
            }
        }
    }
}