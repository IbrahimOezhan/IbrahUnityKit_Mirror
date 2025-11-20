using UnityEngine;

namespace IbrahKit
{
    public abstract class UI_Text_Modifier : UI_Interactive_Extension
    {
        protected UI_Text_Wrapper text;

        [SerializeField]
        private GameObject target;

        protected UI_Text_Modifier(GameObject go) : base(go)
        {

        }

        protected override bool InitPro()
        {
            text = new(target == null ? go : target);

            return text != null && text.GetMode() != UI_Text_Wrapper.Mode.NONE;
        }

        protected override int GetOrderPro()
        {
            return 50;
        }
    }
}