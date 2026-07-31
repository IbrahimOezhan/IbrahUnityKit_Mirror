#region

using System.Collections.Generic;
using IbrahKit.Input;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Selectable
{
    public class UI_Navigation_Manager_Data : ScriptableObject
    {
        [SerializeField] private List<Input_Manager.InputType> supportedUINavigationMethods = new();

        public IReadOnlyList<Input_Manager.InputType> GetSupportedNavigationMethods() => supportedUINavigationMethods;
    }
}