using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit
{
    [System.Serializable]
    public abstract class UI_Selectable_Transition<TTarget, SOTarget> : UI_Selectable_Transition where TTarget : Component where SOTarget : Selectable_Transition_SO
    {
        private bool initialized;

        [SerializeField, ReadOnly] private OverrideComponent<TTarget> fTarget;

        [SerializeField, ReadOnly] private TTarget getComponentTarget;

        [SerializeField] private TTarget target;

        [SerializeField] private SOTarget config;

        public override void Init(GameObject go)
        {
            if (initialized) return;

            Debug.Log("Init " + go.name, Color.magenta);

            initialized = true;

            getComponentTarget = go.GetComponent<TTarget>();

            fTarget = new(getComponentTarget);

            fTarget.SetOverride(target);
        }

        protected TTarget GetTarget()
        {
            TTarget target = fTarget.GetValue();

            return target;
        }

        protected SOTarget GetSO()
        {
            return config;
        }
    }
}