#region

using System;
using IbrahKit.Localization;
using IbrahKit.Utilities;
using Sirenix.Serialization;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Modifier
{
    [Serializable]
    public class UI_Modifier_Text_Localizer
    {
        private UI_Modifier_Text_Modifier modifier;
        
        [SerializeField] protected Local_Key key;

        [SerializeField] protected string fallbackText;

        [OdinSerialize] protected object[] parameters;

        public UI_Modifier_Text_Localizer(UI_Modifier_Text_Modifier modifier)
        {
            this.modifier = modifier;
            
            if (Local_Manager.TryGet(out Local_Manager lm)) lm.onLanguageChanged += Modify;
        }

        public void OnDestroy()
        {
            if (Local_Manager.TryGet(out Local_Manager lm)) lm.onLanguageChanged -= Modify;

        }

        protected void Modify()
        {
            modifier.GetTextWrapper().SetText(GetContent());
        }

        public void SetFallback(string _fallback)
        {
            if (fallbackText == _fallback) return;

            fallbackText = _fallback;
            
            Modify();
        }

        public void SetKey(string _key)
        {
            if (key == _key) return;

            key = _key;
            
            Modify();
        }

        public void SetParam(params object[] _params)
        {
            parameters = _params;
            
            Modify();
        }

        public void SetKeyParam(string _key, params object[] _params)
        {
            key = _key;

            parameters = _params;

            Modify();
        }

        protected string GetContent()
        {
            if (key.Key.IsEmpty())
            {
                return string.Empty;
            }

            if (Local_Manager.TryGet(out Local_Manager result))
            {
                return result.GetString(key, fallbackText, parameters);
            }
            else
            {
#if UNITY_EDITOR
                Local_Manager_Data config = Local_Manager_Data.Instance;

                if (config == null) return "Local Config couldn't be found";

                config.TryGetString(key, config.GetFirstLanguage(), out string res);

                return res;
#else
                return "Local Manager missing";
#endif
            }
        }
    }
}