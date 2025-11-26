using UnityEngine;

namespace IbrahKit.UI
{
    public class S_Transition_Animator : UI_Selectable_Transition<Animator, S_Transition_Animator_SO>
    {
        public override void Apply(UI_SELECTABLE_STATE state)
        {
            GetTarget().Play(GetSO().GetValue(state));
        }
    }
}