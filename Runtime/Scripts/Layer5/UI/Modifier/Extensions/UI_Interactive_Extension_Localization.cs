#region

using System;
using IbrahKit.Localization;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public class UI_Interactive_Extension_Localization : UI_Interactive_Extension_Text_Modifier
    {
        [SerializeField] protected Local_Key key;

        [SerializeField] protected string fallbackText;

        [SerializeField] protected string[] parameters;

        public UI_Interactive_Extension_Localization(UI_Interactive extension) : base(extension)
        {
        }

        protected override bool InitPro()
        {
            bool result = base.InitPro();

            if (!result) return false;

            if (Local_Manager.TryGet(out Local_Manager lm)) lm.onLanguageChanged += extension.RunExtensions;

            return result;
        }

        protected override void CleanupPro()
        {
            if (Local_Manager.TryGet(out Local_Manager lm)) lm.onLanguageChanged -= extension.RunExtensions;
        }

        protected override void RunPro()
        {
            if (!Init()) return;

            text.SetText(GetContent());
        }

        public void SetFallback(string _fallback)
        {
            if (fallbackText == _fallback) return;

            fallbackText = _fallback;

            extension.RunExtensions();
        }

        public void SetKey(string _key)
        {
            if (key == _key) return;

            key = _key;

            extension.RunExtensions();
        }

        public void SetParam(params string[] _params)
        {
            parameters = _params;

            extension.RunExtensions();
        }

        public void SetKeyParam(string _key, params string[] _params)
        {
            key = _key;

            parameters = _params;

            extension.RunExtensions();
        }

        protected string GetContent()
        {
            if (key.Value.IsEmpty())
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
                Local_Manager_Data config = Local_Editor_Settings.Config();

                if (config == null) return "Local Config couldnt be found";

                config.TryGetString(key, config.GetFirstLanguage(), out string res);

                return res;
#else
                return "Local Manager missing";
#endif
            }
        }
    }
}