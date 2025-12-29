using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public abstract class UI_Selectable_Transition<TTarget, SOTarget> : UI_Selectable_Transition where TTarget : Component where SOTarget : Selectable_Transition_SO
    {
        private bool initialized;

#if ODIN_INSPECTOR
        [SerializeField, ReadOnly]
#endif
        private Override_Component<TTarget> fTarget;

#if ODIN_INSPECTOR
        [SerializeField, ReadOnly]
#endif
        private TTarget getComponentTarget;


        [SerializeField] private TTarget target;

        [SerializeField] private SOTarget config;

        private bool IsSoNull() => config == null;

        [Button, ShowIf(nameof(IsSoNull))]
        public void Create()
        {
            config = Asset_Utilities.CreateScriptableObject<SOTarget>($"Assets/ScriptableObjects/{typeof(SOTarget).Name}.asset");
        }

        public override void Init(GameObject go)
        {
            if (initialized) return;

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