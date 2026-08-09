#region

using System;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.Manager
{
    public abstract class MonoBehaviourSingletonDontDestroyOnLoadData<TManager, TData> : MonoBehaviourSingletonDontDestroyOnLoad<TManager> where TData : ScriptableObject
        where TManager : MonoBehaviourSingletonDontDestroyOnLoadData<TManager, TData>
    {
        [SerializeField, Required] private TData data;

        public TData GetManagerData() => data;

        private bool ShowButton() => data != null;

#if UNITY_EDITOR
        [Button, HideIf(nameof(ShowButton))]
        public void CreateData()
        {
            Type t = typeof(TData);

            data = Asset_Utilities.CreateScriptableObject<TData>(
                $"Assets/ScriptableObjects/Manager_Data/{t.GetNiceName()}.asset");
        }
#endif
    }
}