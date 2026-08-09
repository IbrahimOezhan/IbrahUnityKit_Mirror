#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.Debugging;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.Manager
{
    [ExecuteInEditMode]
    public class Toolkit_Manager : MonoBehaviour
    {
        private const string NONE = "None";

        [ValueDropdown(nameof(Dropdown)), SerializeField, OnValueChanged(nameof(OnValueChanged))]
        private string addManager;

        [SerializeField] private bool excludeInScene = true;

        private void Awake()
        {
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public IEnumerable Dropdown()
        {
            List<Type> types = GetManagerTypes().ToList();

            if (excludeInScene) types.RemoveAll(x => FindAnyObjectByType(x) != null);

            List<string> types2 = types.Select(x => x.FullName).ToList();
            
            types2.Sort();
            
            types2.Insert(0, NONE);

            return types2;
        }

        private Type[] GetManagerTypes()
        {
            return Type_Utilities.GetSubTypes(typeof(MonoBehaviourSingletonBase<>)).ToArray();
        }

        public void OnValueChanged()
        {
            if (addManager == NONE) return;

            Type getType = Type_Utilities.GetTypeByFullName(addManager);

            if (getType == null)
            {
                IbrahDebug.Log(addManager + " type null");
                return;
            }

            GameObject go = new(getType.Name);

            go.transform.parent = transform;

            go.AddComponent(getType);

            transform.SortChildren();

            addManager = NONE;
        }
    }
}