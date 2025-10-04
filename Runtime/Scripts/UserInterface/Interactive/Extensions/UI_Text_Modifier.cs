using UnityEngine;

namespace IbrahKit
{
    public abstract class UI_Text_Modifier : UI_Extension
    {
        protected UI_Text_Wrapper text;

        [SerializeField] private GameObject target;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override bool TryInit()
        {
            text = new(target == null ? gameObject : target);

            return base.TryInit() && text != null && text.GetMode() != UI_Text_Wrapper.Mode.NONE;
        }
    }
}