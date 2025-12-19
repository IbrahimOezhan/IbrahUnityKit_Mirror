using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    public class UI_Navigation_Manager_Data : ScriptableObject
    {
        [SerializeField] private List<InputType> supportedUINavigationMethods = new();

        public IReadOnlyList<InputType> GetSupportedNavigationMethods() => supportedUINavigationMethods;
    }
}
