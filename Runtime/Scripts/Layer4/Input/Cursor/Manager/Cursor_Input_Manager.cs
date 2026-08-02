#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.InfoCollector;
using IbrahKit.Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.Input
{
    public class Cursor_Input_Manager : Manager_Global<Cursor_Input_Manager>, IInfoCollector
    {
        [SerializeField, ReadOnly] private List<GameObject> receivers;
        private Camera camera;

        private CursorInput input;

        private Vector2 mousePos;

        public Action onLeftMouseButton;

        private EventSystem system;

        private void Update()
        {
            if (input == null) return;

            if (!camera) camera = Camera.main;

            if (!system) system = EventSystem.current;

            mousePos = input.Map.MousePos.ReadValue<Vector2>();

            Cursor_State_Manager.GetInstance().Run();

            receivers = GetUIReceivers();
        }

        public string GetInformation() => "Is Over UI: " + IsOverUIReceiver();

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

        public InputAction GetLMB()
        {
            return input.Map.LMB;
        }

        public void LeftMouseButton(InputAction.CallbackContext context)
        {
            onLeftMouseButton?.Invoke();
        }

        public List<GameObject> GetReceivers()
        {
            return !system.IsPointerOverGameObject() ? GetGameReceivers() : GetUIReceivers();
        }

        public bool IsOverReceiver()
        {
            if (!camera || !system) return false;

            return !system.IsPointerOverGameObject() ? IsOverGameReceiver() : IsOverUIReceiver();
        }

        public List<GameObject> GetGameReceivers()
        {
            List<GameObject> results = new();

            Vector2 mousePosWorld = camera.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit2D = Physics2D.Raycast(mousePosWorld, Vector2.zero);

            if (hit2D.transform) results.Add(hit2D.transform.gameObject);

            return results;
        }

        public bool IsOverGameReceiver()
        {
            return GetGameReceivers().Any(x => x.GetComponent<IRaycast_Receiver>() != null);
        }

        public List<GameObject> GetUIReceivers()
        {
            PointerEventData pointerData = new(system)
            {
                position = mousePos
            };

            List<RaycastResult> results = new();

            system.RaycastAll(pointerData, results);

            return results.Select(x => x.gameObject).ToList();
        }

        public bool IsOverUIReceiver()
        {
            return GetUIReceivers().Any(x => x.gameObject.GetComponent<IRaycast_Receiver>() != null);
        }

        public Camera GetCamera() => camera;
    }
}