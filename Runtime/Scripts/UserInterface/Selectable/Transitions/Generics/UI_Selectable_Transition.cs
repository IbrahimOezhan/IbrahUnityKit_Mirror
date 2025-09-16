using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public abstract class UI_Selectable_Transition<TTarget, SOTarget> : UI_Selectable_Transition where TTarget : Component where SOTarget : Selectable_Transition_SO
    {
        private OverrideReference<TTarget> fTarget;

        [SerializeField] private TTarget target;

        [SerializeField, ReadOnly] private TTarget getComponentTarget;

        [SerializeField] private SOTarget config;

        public void Init(GameObject go)
        {
            getComponentTarget = go.GetComponent<TTarget>();
            fTarget = new(getComponentTarget);
            fTarget.SetOverride(target);
        }

        protected TTarget GetTarget()
        {
            return fTarget.GetValue();
        }

        protected SOTarget GetSO()
        {
            return config;
        }
    }
}