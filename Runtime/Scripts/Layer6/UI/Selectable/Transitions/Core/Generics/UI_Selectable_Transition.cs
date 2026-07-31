#region

using System;
using IbrahKit.Override;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Selectable
{
    [Serializable]
    public abstract class UI_Selectable_Transition<TTarget, SOTarget> : UI_Selectable_Transition
        where TTarget : Component where SOTarget : Selectable_Transition_SO
    {
#if ODIN_INSPECTOR
        [SerializeField, ReadOnly]
#endif
        private Override_Class<TTarget> fTarget;

#if ODIN_INSPECTOR
        [SerializeField, ReadOnly]
#endif
        private TTarget getComponentTarget;


        [SerializeField] private TTarget target;

        [SerializeField] private SOTarget config;
        private bool initialized;

        private bool IsSoNull() => config == null;

        [Button, ShowIf(nameof(IsSoNull))]
        public void Create()
        {
            config = Asset_Utilities.CreateScriptableObject<SOTarget>(
                $"Assets/ScriptableObjects/{typeof(SOTarget).Name}.asset");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public override void Init(GameObject go)
        {
            if (initialized) return;

            initialized = true;

            getComponentTarget = go.GetComponent<TTarget>();

            OverrideReplace<TTarget> replace = new OverrideReplace<TTarget>();

            fTarget = new(getComponentTarget, replace);

            replace.AddOverride(target);
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