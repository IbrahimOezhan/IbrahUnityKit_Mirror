#region

using UnityEngine;

#endregion

public class Cursor_Native_Manager : Cursor_State_Manager
{
    public override void Disabled()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void Clamped()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void Unclamped()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}