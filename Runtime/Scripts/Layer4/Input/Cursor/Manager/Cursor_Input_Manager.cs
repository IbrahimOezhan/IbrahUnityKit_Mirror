#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.InfoCollector;
using IbrahKit.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.Input
{
    public class Cursor_Input_Manager : Manager_Global<Cursor_Input_Manager>, IInfoCollector
    {
        private CursorInput input;

        private Vector2 mousePos;

        public Action onLeftMouseButton;

        private void Update()
        {
            if (input == null) return;

            mousePos = input.Map.MousePos.ReadValue<Vector2>();

            Cursor_State_Manager.GetInstance().Run();
        }

        public string GetInformation() => "Is Over UI: " + CursorOverUI(EventSystem.current);

        public int GetDebugOrder() => -80;


        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            input = new();

            input.Enable();

            input.Map.LMB.performed += LeftMouseButton;
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            if (input == null) return;

            input.Map.LMB.performed -= LeftMouseButton;

            input.Disable();

            input.Dispose();
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

        public void LeftMouseButton(InputAction.CallbackContext context)
        {
            onLeftMouseButton?.Invoke();
        }

        public bool CursorOverGameUI(Camera camera)
        {
            List<GameObject> results = new();

            Vector2 mousePosWorld = camera.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit2D = Physics2D.Raycast(mousePosWorld, Vector2.zero);

            if (hit2D.transform) results.Add(hit2D.transform.gameObject);

            return results.Any(x => x.GetComponent<IRaycast_Receiver>() != null);
        }

        public bool CursorOverUI(EventSystem system)
        {
            PointerEventData pointerData = new(system)
            {
                position = mousePos
            };

            List<RaycastResult> results = new();

            system.RaycastAll(pointerData, results);

            return results.Any(x => x.gameObject.GetComponent<IRaycast_Receiver>() != null);
        }
    }
}