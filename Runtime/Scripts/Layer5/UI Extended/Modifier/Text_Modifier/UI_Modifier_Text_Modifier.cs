#region

using System;
using IbrahKit.UI.Modifier;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public class UI_Modifier_Text_Modifier : MonoBehaviour
    {
        [SerializeField, ShowIf(nameof(mode), Text_Mode.LOCALISED)]
        private UI_Modifier_Text_Localizer localization;

        [SerializeField] private UI_Modifier_Text_Setter staticSetter;

        [SerializeField] private Text_Mode mode;

        [SerializeField] private GameObject nonDefaultTarget;

        protected UI_Text_Wrapper text;

        private void Awake()
        {
            text = new(nonDefaultTarget == null ? gameObject : nonDefaultTarget);

            //return text != null && text.GetMode() != UI_Text_Wrapper.Mode.NONE;
        }

        public UI_Modifier_Text_Localizer GetLocalization() => localization;

        public UI_Modifier_Text_Setter GetStaticSetter() => staticSetter;

        public UI_Text_Wrapper GetTextWrapper()
        {
            if(!Application.isPlaying)
                return new(nonDefaultTarget == null ? gameObject : nonDefaultTarget);
            
            return text;
        }

        public void Validate(SelfValidationResult validationResult, GameObject content)
        {
            UI_Text_Wrapper.Validate(validationResult, content);
        }

        private enum Text_Mode
        {
            STATIC,
            LOCALISED
        }
    }
}