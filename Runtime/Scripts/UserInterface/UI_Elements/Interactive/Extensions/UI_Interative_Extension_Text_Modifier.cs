using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace IbrahKit.UI
{
    [System.Serializable]
    public abstract class UI_Interative_Extension_Text_Modifier : UI_Interactive_Extension
    {
        protected UI_Text_Wrapper text;

        [SerializeField]
        private GameObject target;

        protected UI_Interative_Extension_Text_Modifier(UI_Interactive extension) : base(extension)
        {

        }

        protected override bool InitPro()
        {
            text = new(target == null ? extension.gameObject : target);

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