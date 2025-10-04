using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

namespace IbrahKit
{
    public class Cursor_Sprite_Manager : Manager_DDOL<Cursor_Sprite_Manager>
    {
        private CursorState state;

        private Camera main;

        [SerializeField] private Transform cursorTransform;

        [SerializeField] private RectTransform canvas;

        [SerializeField] private CursorStyle style;

        [SerializeField] private Image cursorImage;

        private void Update()
        {
            if (main == null) main = Camera.main;

            Cursor_Input_Manager cim = Cursor_Input_Manager.GetInstance();

            if (cim == null) return;

            bool hovering = IsHovering(main, EventSystem.current, cim.GetMousePos());

            if (hovering)
            {
                ButtonControl leftButton = Mouse.current.leftButton;

                if (leftButton.wasPressedThisFrame || (state == CursorState.Down && leftButton.isPressed))
                {
                    SetState(CursorState.Down);
                }
                else
                {
                    SetState(CursorState.Hovering);
                }
            }
            else
            {
                SetState(CursorState.None);
            }

            Vector2 pos = GetCursorPos(main, canvas, Cursor_Input_Manager.GetInstance().GetMousePos());

            cursorTransform.localPosition = pos;

            cursorTransform.gameObject.SetActive(Cursor_Visibilty_Manager.GetInstance().IsVisible());

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;

            style.Set(cursorImage, state);
        }

        private void SetState(CursorState state)
        {
            this.state = state;
        }

        private Vector2 GetCursorPos(Camera mainCamera, RectTransform canvas, Vector2 mousePos)
        {
            if (mainCamera == null) return Vector2.zero;
            if (canvas == null) return Vector2.zero;

            Rect mainCameraRect = mainCamera.rect;

            // Get camera rect in screen pixels:
            float camX = mainCameraRect.x * Screen.width;
            float camY = mainCameraRect.y * Screen.height;
            float camWidth = mainCameraRect.width * Screen.width;
            float camHeight = mainCameraRect.height * Screen.height;

            // Clamp mouse position to inside camera viewport (optional, if you want cursor only in viewport)
            float clampedX = Mathf.Clamp(mousePos.x, camX, camX + camWidth);
            float clampedY = Mathf.Clamp(mousePos.y, camY, camY + camHeight);

            // Calculate mouse position normalized inside camera viewport (0 to 1)
            float normalizedX = (clampedX - camX) / camWidth;
            float normalizedY = (clampedY - camY) / camHeight;

            // Map normalized position to canvas local coordinates (assuming pivot at center)
            float canvasWidth = canvas.rect.width;
            float canvasHeight = canvas.rect.height;

            float mappedX = (normalizedX - 0.5f) * canvasWidth;
            float mappedY = (normalizedY - 0.5f) * canvasHeight;

            return new(mappedX, mappedY);
        }

        private bool IsHovering(Camera mainCamera, EventSystem syetem, Vector2 mousePos)
        {
            if (mainCamera == null) return false;
            if (syetem == null) return false;

            List<GameObject> gameObjectsResult = new();

            if (syetem.IsPointerOverGameObject())
            {
                PointerEventData pointerData = new(syetem)
                {
                    position = mousePos
                };

                List<RaycastResult> results = new();

                syetem.RaycastAll(pointerData, results);

                gameObjectsResult.AddRange(results.Select(x => x.gameObject));
            }
            else
            {
                Vector2 mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);

                RaycastHit2D hit2D = Physics2D.Raycast(mousePosWorld, Vector2.zero);

                if (hit2D.transform != null) gameObjectsResult.Add(hit2D.transform.gameObject);
            }

            foreach (var item in gameObjectsResult)
            {
                if (item.GetComponent<ICursorHandler>() != null)
                {
                    return true;
                }
            }

            return false;
        }

        private enum CursorState
        {
            None,
            Hovering,
            Down,
        }

        [System.Serializable]
        private class CursorStyle
        {
            [SerializeField] private Sprite none;
            [SerializeField] private Sprite hovering;
            [SerializeField] private Sprite pressing;

            public void Set(Image renderer, CursorState state)
            {
                switch (state)
                {
                    case CursorState.None:
                        renderer.sprite = none;
                        break;
                    case CursorState.Hovering:
                        renderer.sprite = hovering;
                        break;
                    case CursorState.Down:
                        renderer.sprite = pressing;
                        break;
                }
            }
        }
    }
}