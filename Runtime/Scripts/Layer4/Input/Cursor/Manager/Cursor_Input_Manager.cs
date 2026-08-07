#region

using System.Collections.Generic;
using System.Linq;
using IbrahKit.InfoCollector;
using IbrahKit.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.Input.Cursor
{
    public class Cursor_Input_Manager : Manager_Global<Cursor_Input_Manager>, IInfoCollector, IInputType
    {
        private Camera camera;

        private CursorInput input;

        private Vector2 mousePos;

        private EventSystem system;

        public string GetInformation() => "Is Over UI: " + IsOverUIReceiver();

        public int GetDebugOrder() => -80;

        public void OnInput(Input_Manager.InputType inputType)
        {
            if (inputType != Input_Manager.InputType.MOUSE) return;

            OnInput();
        }

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            input = new();

            input.Enable();

            Input_Manager.GetInstance().Register(this);
        }

        protected override void InstanceDestroy()
        {
            Input_Manager.GetInstance().UnRegister(this);

            base.InstanceDestroy();

            if (input == null) return;

            input.Disable();

            input.Dispose();
        }

        private void OnInput()
        {
            if (input == null) return;

            if (!camera) camera = Camera.main;

            if (!system) system = EventSystem.current;

            mousePos = input.Map.MousePos.ReadValue<Vector2>();

            Cursor_State_Manager.GetInstance().Run();

            HashSet<GameObject> newRec = GetReceivers();
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

        public HashSet<GameObject> GetReceivers()
        {
            return !system.IsPointerOverGameObject() ? GetGameReceivers() : GetUIReceivers();
        }

        public bool IsOverReceiver()
        {
            if (!camera || !system) return false;

            return !system.IsPointerOverGameObject() ? IsOverGameReceiver() : IsOverUIReceiver();
        }

        public HashSet<GameObject> GetGameReceivers()
        {
            List<GameObject> results = new();

            Vector2 mousePosWorld = camera.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit2D = Physics2D.Raycast(mousePosWorld, Vector2.zero);

            if (hit2D.transform) results.Add(hit2D.transform.gameObject);

            return new HashSet<GameObject>(results);
        }

        public bool IsOverGameReceiver()
        {
            return GetGameReceivers().Any(x => x.GetComponent<ICursorReceiver>() != null);
        }

        public HashSet<GameObject> GetUIReceivers()
        {
            PointerEventData pointerData = new(system)
            {
                position = mousePos
            };

            List<RaycastResult> results = new();

            system.RaycastAll(pointerData, results);

            return results.Select(x => x.gameObject).ToHashSet();
        }

        public bool IsOverUIReceiver()
        {
            return GetUIReceivers().Any(x => x.gameObject.GetComponent<ICursorReceiver>() != null);
        }

        public Camera GetCamera() => camera;
    }
}