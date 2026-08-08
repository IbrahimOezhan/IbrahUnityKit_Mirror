#region

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.Input.Cursor
{
    [Serializable]
    public class Cursor_Controller_Input : IInputType
    {
        private CursorInput input;

        private Vector2 mousePos;

        private EventSystem system;

        public void OnInput(Input_Manager.InputType inputType)
        {
            if (inputType != Input_Manager.InputType.MOUSE) return;

            if (input == null) return;

            if (!system) system = EventSystem.current;

            mousePos = input.Map.MousePos.ReadValue<Vector2>();

            Cursor_Manager.GetInstance().GetCursorState().Run();
        }

        public void Init()
        {
            input = new();

            input.Enable();

            Input_Manager.GetInstance().Register(this);
        }

        public void Destroy()
        {
            if (input == null) return;

            input.Disable();

            input.Dispose();

            Input_Manager.GetInstance().UnRegister(this);
        }

        public Vector2 GetMousePos() => mousePos;

        public Vector2 GetCanvasMousePos(Canvas canvas)
        {
            Vector2 screenPos = Mouse.current.position.ReadValue();

            RectTransform canvasRect = canvas.GetComponent<RectTransform>();

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                screenPos,
                canvas.worldCamera, // null if Screen Space - Overlay
                out Vector2 localPoint
            );

            return localPoint;
        }

        public InputAction GetLMB()
        {
            return input.Map.LMB;
        }
    }
}