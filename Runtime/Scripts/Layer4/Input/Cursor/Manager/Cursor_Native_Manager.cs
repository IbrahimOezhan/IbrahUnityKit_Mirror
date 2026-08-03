#region

using UnityEngine;

#endregion

namespace IbrahKit.Input.Cursor
{
    public class Cursor_Native_Manager : Cursor_State_Manager
    {
        public override void Disabled()
        {
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }

        public override void Clamped()
        {
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        }

        public override void Unclamped()
        {
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
    }
}