#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace IbrahKit.Input
{
    public class Input_Manager_Data : ScriptableObject
    {
        [SerializeField] private List<Input_Manager.InputType> enabledInputMethods = new();

        public IReadOnlyList<Input_Manager.InputType> EnabledInputMethods() => enabledInputMethods;
    }
}