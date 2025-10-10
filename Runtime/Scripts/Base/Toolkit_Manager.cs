using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace IbrahKit
{
    [ExecuteInEditMode]
    public class Toolkit_Manager : MonoBehaviour
    {
        [ValueDropdown(nameof(Dropdown)), SerializeField, OnValueChanged(nameof(OnValueChanged))]
        private string addManager;

        private void Awake()
        {
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public IEnumerable Dropdown()
        {
            List<string> types = GetManagerTypes().Select(x => x.FullName).ToList();

            types.Insert(0, "None");

            return types;
        }

        private Type[] GetManagerTypes()
        {
            return Type_Utilities.GetAllTypes(typeof(Manager_DDOL<>));
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