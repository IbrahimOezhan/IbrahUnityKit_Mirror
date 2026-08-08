#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.Input.Cursor
{
    [Serializable]
    public class Cursor_Controller_State_Native : Cursor_Controller_State
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