using UnityEngine;

namespace IbrahKit
{
    public class UI_Text_Modifier : UI_Extension
    {
        protected UI_Text_Wrapper text;

        [SerializeField] private GameObject target;

        protected override void Awake()
        {
            base.Awake();

            text = new(target ?? gameObject);
        }
    }
}