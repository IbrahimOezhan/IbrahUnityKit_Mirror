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
        [SerializeField, ReadOnly] private Override_Class<TTarget> fTarget;

        [SerializeField, ReadOnly] private TTarget getComponentTarget;

        [SerializeField] private TTarget target;

        [SerializeField] private SOTarget config;
        private bool initialized;

        #if  UNITY_EDITOR
        [Button, ShowIf(nameof(IsSoNull))]
        public void Create()
        {
            config = Asset_Utilities.CreateScriptableObject<SOTarget>(
                $"Assets/ScriptableObjects/{typeof(SOTarget).Name}.asset");
        }
        #endif

        // ReSharper disable Unity.PerformanceAnalysis
        public override void Init(GameObject go)
        {
            if (initialized) return;

            initialized = true;

            getComponentTarget = go.GetComponent<TTarget>();

            OverrideReplace<TTarget> replace = new OverrideReplace<TTarget>();

            fTarget = new(getComponentTarget, replace);

            if (target != null) replace.AddOverride(target);
        }

        protected TTarget GetTarget()
        {
            return fTarget.GetValue();
        }

        protected SOTarget GetSO()
        {
            return config;
        }

        private bool IsSoNull() => config == null;
    }
}