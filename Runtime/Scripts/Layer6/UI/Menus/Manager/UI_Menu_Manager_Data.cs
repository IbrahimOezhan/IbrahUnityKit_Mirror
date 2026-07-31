#region

using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.UI.Menu
{
    public class UI_Menu_Manager_Data : ScriptableObject
    {
        [SerializeField] private Key hideUI;

        public Key GetKey()
        {
            return hideUI;
        }
    }
}