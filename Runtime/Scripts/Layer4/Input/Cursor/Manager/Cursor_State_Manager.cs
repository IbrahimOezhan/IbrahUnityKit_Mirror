using System;
using UnityEngine;

public class Cursor_State_Manager : MonoBehaviour
{
    private CursorState cursorState;
    
    public enum CursorState
    {
        HIDDEN,
        CLAMPED,
        UNCLAMPED,
    }

    private void Update()
    {
        switch (cursorState)
        {
            case CursorState.HIDDEN:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case CursorState.CLAMPED:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                break;
            case CursorState.UNCLAMPED:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SetCursorState(CursorState state)
    {
        cursorState = state;
    }

    public CursorState GetCursorState()
    {
        return cursorState;
    }
}
