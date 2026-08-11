#region

using System.Collections.Generic;
using IbrahKit.Input;
using UnityEngine;

#endregion

public class UI_Selectable_Manager_Data : ScriptableObject
{
    [SerializeField] private List<Input_Manager.InputType> enabledNavigationInputMethods = new();

    public IReadOnlyList<Input_Manager.InputType> EnabledNavigationInputMethods() => enabledNavigationInputMethods;
}