#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.Debugging;
using IbrahKit.InfoCollector;
using IbrahKit.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.Input
{
    public class Cursor_Input_Manager : Manager_Global<Cursor_Input_Manager>, IDebug
    {
        private CursorInput input;

        private Vector2 mousePos;

        public Action OnLMB;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            input = new();

            input.Enable();

            input.Map.LMB.performed += LMB;
        }

        private void Update()
        {
            if (input == null)
            {
                return;
            }

            mousePos = input.Map.MousePos.ReadValue<Vector2>();
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            if (input != null)
            {
                input.Map.LMB.performed -= LMB;

                input.Disable();

                input.Dispose();
            }
        }

        public Vector2 GetMousePos()
        {
            return mousePos;
        }

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

        private void OnMouseDown()
        {
            throw new NotImplementedException();
        }

        public void LMB(InputAction.CallbackContext context)
        {
            OnLMB?.Invoke();
        }

        public bool CursorOverUI(EventSystem system)
        {
            PointerEventData pointerData = new(system)
            {
                position = mousePos
            };

            List<RaycastResult> results = new();

            system.RaycastAll(pointerData, results);

            return results.Where(x => x.gameObject.GetComponent<ICursorHandler>() != null).Count() > 0;
        }

        public string DebugContent()
        {
            return "Is Over UI: " + CursorOverUI(EventSystem.current);
        }

        public int DebugOrder()
        {
            return -80;
        }
    }
}