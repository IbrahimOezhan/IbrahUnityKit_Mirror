using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace IbrahKit
{
    [ExecuteInEditMode]
    public class Toolkit_Manager : MonoBehaviour
    {
        [ValueDropdown(nameof(Dropdown)), SerializeField, OnValueChanged(nameof(OnValueChanged))]
        private string addManager;

        [SerializeField] private bool excludeInScene;

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

            if(excludeInScene)
            {
                for (int i = types.Count - 1; i >= 0; i--)
                {
                    if (FindAnyObjectByType(types[i]) != null)
                    {
                        types.Remove(types[i]);
                    }
                }
            }

            List<string> types2 = types.Select(x => x.FullName).ToList();

            types2.Insert(0, "None");

            return types2;
        }

        private Type[] GetManagerTypes()
        {
            return Type_Utilities.GetAllTypes(typeof(Manager<>)).Where(x => x.Namespace == nameof(IbrahKit)).ToArray();
        }

        public void OnValueChanged()
        {
            if (addManager == "None") return;

            Type[] types = GetManagerTypes();

            Type getType = Type.GetType(addManager);

            GameObject go = new(getType.Name);

            go.transform.parent = transform;

            go.AddComponent(getType);

            addManager = "None";
        }
    }
}