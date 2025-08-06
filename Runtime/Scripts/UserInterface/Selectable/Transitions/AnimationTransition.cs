using System;
using UnityEngine;

namespace IbrahKit
{
    [Serializable]
    public class AnimationTransition : SelectableTransition
    {
        [SerializeField] private Animator animator;

        [SerializeField] private string none = "None";

        [SerializeField] private string hovering = "Hovering";

        [SerializeField] private string pressed = "Pressed";

        protected override void OnHovering(GameObject go)
        {
            Play(hovering, go);
        }

        protected override void OnNone(GameObject go)
        {
            Play(none, go);
        }

        protected override void OnPressed(GameObject go)
        {
            Play(pressed, go);
        }

        private void Play(string animState, GameObject go)
        {
            if (!go.activeInHierarchy)
            {
                return;
            }

            if (animator == null)
            {
                animator = go.GetComponent<Animator>();
            }

            animator.Play(animState);
        }
    }
}