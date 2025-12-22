using UnityEngine;
using UnityEngine.UI;

namespace IbrahKit.UI
{
    public class S_Transition_Color : UI_Selectable_Transition<Graphic, S_Transition_Color_SO>
    {
        public override void Apply(UI_SELECTABLE_STATE state)
        {
            Color newColor = GetSO().GetValue(state);
            GetTarget().color = newColor;
        }
    }
}