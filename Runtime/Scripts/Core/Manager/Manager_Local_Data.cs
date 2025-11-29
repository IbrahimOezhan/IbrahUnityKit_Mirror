using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using UnityEngine;

namespace IbrahKit
{
    public abstract class Manager_Local_Data<TManager, TData> : Manager_Local<TManager> where TData : ScriptableObject where TManager : Manager_Local_Data<TManager, TData>
    {
        [SerializeField, Required] private TData data;

        public TData GetManagerData() => data;

        private bool ShowButton() => data != null;

#if UNITY_EDITOR
        [Button, HideIf(nameof(ShowButton))]
        public void CreateData()
        {
            Type t = typeof(TData);

            data = Asset_Utilities.CreateAsset<TData>($"Assets/ScriptableObjects/Manager_Data/{t.GetNiceName()}.asset");
        }
#endif
    }
}