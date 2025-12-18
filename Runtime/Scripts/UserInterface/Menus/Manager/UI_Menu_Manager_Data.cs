using UnityEngine;
using UnityEngine.InputSystem;

namespace IbrahKit
{
    public class UI_Menu_Manager_Data : ScriptableObject
    {
        [SerializeField] private Key hideUI;

        public Key GetKey() { return hideUI; }
    }
}
