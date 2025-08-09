using System;
using UnityEngine;

namespace IbrahKit
{
    [Serializable]
    public class ScaleTransition : UI_Selectable_Transition
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private float none;
        [SerializeField] private float hovering;
        [SerializeField] private float pressed;

        protected override void OnNone(GameObject go)
        {
            if (rect == null) rect = go.GetComponent<RectTransform>();

            Vector3 scale = new(none, none, 1);

            rect.localScale = scale;
        }

        protected override void OnHovering(GameObject go)
        {
            if (rect == null) rect = go.GetComponent<RectTransform>();

            Vector3 scale = new(hovering, hovering, 1);

            rect.localScale = scale;
        }

        protected override void OnPressed(GameObject go)
        {
            if (rect == null) rect = go.GetComponent<RectTransform>();

            Vector3 scale = new(pressed, pressed, 1);

            rect.localScale = scale;
        }
    }
}