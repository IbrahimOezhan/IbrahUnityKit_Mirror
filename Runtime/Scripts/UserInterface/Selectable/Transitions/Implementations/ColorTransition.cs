using UnityEngine;
using UnityEngine.UI;

namespace IbrahKit
{
    public class ColorTransition : UI_Selectable_Transition<Graphic, ColorTransition_SO>
    {
        public override void Apply(UI_SELECTABLE_STATE state)
        {
            Color newColor = GetSO().GetValue(state);
            GetTarget().color = newColor;
        }
    }
}