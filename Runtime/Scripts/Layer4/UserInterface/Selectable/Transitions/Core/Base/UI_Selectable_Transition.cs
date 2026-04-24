#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public abstract class UI_Selectable_Transition
    {
        public abstract void Apply(UI_SELECTABLE_STATE state);

        public abstract void Init(GameObject go);
    }
}