using UnityEngine;

namespace IbrahKit
{
    public class AnimatorTransition : UI_Selectable_Transition<Animator, AnimatorTransition_SO>
    {
        public override void Apply(UI_SELECTABLE_STATE state)
        {
            GetTarget().Play(GetSO().GetValue(state));
        }
    }
}