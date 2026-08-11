#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.Input.Cursor
{
    [Serializable]
    public abstract class Cursor_Controller_State
    {
        public enum CursorVisualState
        {
            HIDDEN,
            CLAMPED,
            UNCLAMPED,
        }

        [SerializeField] private CursorVisualState cursorVisualState;

        public void Run()
        {
            switch (cursorVisualState)
            {
                case CursorVisualState.HIDDEN:
                    Disabled();
                    break;
                case CursorVisualState.CLAMPED:
                    Clamped();
                    break;
                case CursorVisualState.UNCLAMPED:
                    Unclamped();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public abstract void Disabled();

        public abstract void Clamped();

        public abstract void Unclamped();

        public void SetCursorState(CursorVisualState state)
        {
            cursorVisualState = state;
        }

        public CursorVisualState GetCursorState()
        {
            return cursorVisualState;
        }
    }
}