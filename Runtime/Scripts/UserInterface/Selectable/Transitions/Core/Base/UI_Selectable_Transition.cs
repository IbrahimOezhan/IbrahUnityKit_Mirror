using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public abstract class UI_Selectable_Transition
    {
        public abstract void Apply(UI_SELECTABLE_STATE state);

        public abstract void Init(GameObject go);
    }
}
