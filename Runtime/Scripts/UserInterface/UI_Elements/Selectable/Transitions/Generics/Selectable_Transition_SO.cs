using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    public class Selectable_Transition_SO<T> : Selectable_Transition_SO
    {
        [SerializeField, Required] private T none;
        [SerializeField, Required] private T selected;
        [SerializeField, Required] private T pressed;

        public T GetValue(UI_SELECTABLE_STATE state)
        {
            switch (state)
            {
                case UI_SELECTABLE_STATE.PRESSED:
                    return pressed;
                case UI_SELECTABLE_STATE.NONE:
                    return none;
                case UI_SELECTABLE_STATE.SELECTED:
                    return selected;
            }

            return none;
        }
    }
}
