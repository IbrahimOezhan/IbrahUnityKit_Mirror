using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public class UI_Selectable_Transition
    {
        public void Apply(UI_SELECTABLE_STATE state, GameObject go)
        {
            switch (state)
            {
                case UI_SELECTABLE_STATE.NONE: OnNone(go); break;
                case UI_SELECTABLE_STATE.SELECTED: OnHovering(go); break;
                case UI_SELECTABLE_STATE.PRESSED: OnPressed(go); break;
            }
        }

        protected virtual void OnNone(GameObject go)
        {

        }
        protected virtual void OnHovering(GameObject go)
        {

        }
        protected virtual void OnPressed(GameObject go)
        {

        }
    }
}
