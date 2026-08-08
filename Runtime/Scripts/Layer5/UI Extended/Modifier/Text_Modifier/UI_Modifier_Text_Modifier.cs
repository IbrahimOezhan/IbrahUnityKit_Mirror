#region

using System;
using IbrahKit.UI.Modifier;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public abstract class UI_Modifier_Text_Modifier : MonoBehaviour
    {
        private enum Text_Mode
        {
            STATIC,
            LOCALISED
        }

        [SerializeField, ShowIf(nameof(mode), Text_Mode.LOCALISED)] private UI_Modifier_Text_Localizer localization;
        
        [SerializeField] private UI_Modifier_Text_Setter staticSetter;
        
        [SerializeField] private Text_Mode mode;
        
        [SerializeField] private GameObject nonDefaultTarget;
        
        protected UI_Text_Wrapper text;

        public UI_Modifier_Text_Localizer GetLocalization() => localization;
        
        public UI_Modifier_Text_Setter GetStaticSetter() => staticSetter;

        private void Awake()
        {
            GameObject defaultTarget = gameObject;

            text = new(nonDefaultTarget == null ? defaultTarget : nonDefaultTarget);

            //return text != null && text.GetMode() != UI_Text_Wrapper.Mode.NONE;
        }

        public UI_Text_Wrapper GetTextWrapper()
        {
            return text;
        }

        public void Validate(SelfValidationResult validationResult, GameObject content)
        {
            UI_Text_Wrapper.Validate(validationResult, content);
        }
    }
}