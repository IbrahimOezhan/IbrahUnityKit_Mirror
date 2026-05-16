#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public abstract class UI_Interactive_Extension_Text_Modifier : UI_Interactive_Extension
    {
        protected UI_Text_Wrapper text;

        [SerializeField] private GameObject nonDefaultTarget;

        protected UI_Interactive_Extension_Text_Modifier(UI_Interactive extension) : base(extension)
        {

        }

        protected override bool InitPro()
        {
            GameObject defaultTarget = extension.gameObject;

            text = new(nonDefaultTarget == null ? defaultTarget : nonDefaultTarget);

            return text != null && text.GetMode() != UI_Text_Wrapper.Mode.NONE;
        }

        protected override int GetOrderPro()
        {
            return 50;
        }

        public override void Validate(SelfValidationResult validationResult, GameObject content)
        {
            UI_Text_Wrapper.Validate(validationResult, content);
        }
    }
}