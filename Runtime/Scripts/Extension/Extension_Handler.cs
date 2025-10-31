using IbrahKit;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Extension_Handler<T> where T : Extension
{
    private const string NONE = "None";

    [SerializeField, OnValueChanged(nameof(OnValueChanged)), ValueDropdown(nameof(GetAllSubtypes))]
    private string extension = NONE;

    [SerializeField, ReadOnly]
    private List<T> extensions = new();

    [SerializeField]
    private GameObject target;

    public void OnValueChanged()
    {
        SortList();

        Type[] types = Type_Utilities.GetAllTypes(typeof(T));

        for (int i = 0; i < types.Length; i++)
        {
            if (types[i].Name == extension)
            {
                T extensionToAdd = target.AddComponent(types[i]) as T;

                extensions.Add(extensionToAdd);

                SortList();

                break;
            }
        }

        extension = NONE;
    }

    private void SortList()
    {
        extensions.RemoveAll(x => x == null);

        extensions.Sort((a, b) =>
        {
            return a.GetOrder().CompareTo(b.GetOrder());
        });
    }

    private IEnumerable GetAllSubtypes()
    {
        return Type_Utilities.GetAllTypesDropdownFormat(typeof(T));
    }

    [Button]
    private void UpdateExtensionList()
    {
        T[] _extension = target.GetComponents<T>();

        extensions = new(_extension.ToList());

        extensions.ForEach(x => x.ResetInit());

        SortList();
    }
}
