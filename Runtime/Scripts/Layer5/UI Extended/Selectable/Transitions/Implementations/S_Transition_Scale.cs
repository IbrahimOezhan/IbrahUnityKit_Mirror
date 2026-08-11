#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Selectable
{
    [Serializable]
    public class S_Transition_Scale : UI_Selectable_Transition<RectTransform, S_Transition_Scale_SO>
    {
        public override void Apply(UI_SELECTABLE_STATE state)
        {
            GetTarget().localScale = GetScale(state);
        }

        private Vector3 GetScale(UI_SELECTABLE_STATE state)
        {
            float value = GetSO().GetValue(state);
            return new Vector3(value, value, 1);
        }
    }
}