#region

using System;
using IbrahKit.Manager;
using UnityEngine;

#endregion

public abstract class Cursor_State_Manager : Manager_Global<Cursor_State_Manager>
{
    public enum CursorState
    {
        HIDDEN,
        CLAMPED,
        UNCLAMPED,
    }

    [SerializeField] private CursorState cursorState;

    public void Run()
    {
        switch (cursorState)
        {
            case CursorState.HIDDEN:
                Disabled();
                break;
            case CursorState.CLAMPED:
                Clamped();
                break;
            case CursorState.UNCLAMPED:
                Unclamped();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public abstract void Disabled();

    public abstract void Clamped();

    public abstract void Unclamped();

    public void SetCursorState(CursorState state)
    {
        cursorState = state;
    }

    public CursorState GetCursorState()
    {
        return cursorState;
    }
}