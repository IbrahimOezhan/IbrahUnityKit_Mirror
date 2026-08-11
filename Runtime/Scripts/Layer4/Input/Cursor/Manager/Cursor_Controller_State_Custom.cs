#region

using System;
using IbrahKit.Debugging;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

#endregion

namespace IbrahKit.Input.Cursor
{
    [Serializable]
    public class Cursor_Controller_State_Custom : Cursor_Controller_State
    {
        public enum CursorClickState
        {
            None,
            Hovering,
            Down,
        }
        
        [SerializeField] private Cursor_Custom_Sprite_Style spriteStyle;
        
        [SerializeField] private RectTransform canvas;

        [SerializeField] private RectTransform cursorTransform;

        [SerializeField] private Image cursorImage;

        [SerializeField, ReadOnly] private CursorClickState cursorInputState;

        public override void Disabled()
        {
            cursorImage.gameObject.SetActive(false);
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }

        public override void Clamped()
        {
            cursorImage.gameObject.SetActive(true);
            
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;

            RenderCursor();
        }

        public override void Unclamped()
        {
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            
            cursorImage.gameObject.SetActive(true);

            RenderCursor();
        }

        private void RenderCursor()
        {
            SetCursorState();

            SetCursorPos();

            cursorImage.sprite = spriteStyle.Get(cursorInputState);
        }

        private void SetCursorState()
        {
            if (!Cursor_Manager.TryGet(out Cursor_Manager cim)) return;

            bool isOverReceiver = cim.GetCursorReceiver().IsOverIReceiver
                (cim.GetCursorReceiver().GameRaycastTargets(
                    EventSystem.current, cim.GetCamera(), cim.GetCursorInput().GetMousePos()));

            if (!isOverReceiver)
            {
                SetState(CursorClickState.None);
                return;
            }

            ButtonControl leftButton = Mouse.current.leftButton;

            if (leftButton.wasPressedThisFrame || (cursorInputState == CursorClickState.Down && leftButton.isPressed))
                SetState(CursorClickState.Down);

            else SetState(CursorClickState.Hovering);
        }

        private void SetState(CursorClickState s)
        {
            cursorInputState = s;
        }

        private void SetCursorPos()
        {
            if (!Cursor_Manager.TryGet(out Cursor_Manager cim)) return;

            Vector2 pos = GetCursorPos(cim.GetCursorInput().GetMousePos(), cim.GetCamera());

            cursorTransform.localPosition = pos;
        }

        private Vector2 GetCursorPos(Vector2 mousePos, Camera camera)
        {
            if (!camera)
            {
                Debug.LogWarning($"{nameof(camera)} is null");
                return Vector2.zero;
            }

            if (!canvas)
            {
                Debug.LogWarning($"{nameof(canvas)} is null");
                return Vector2.zero;
            }

            Rect mainCameraRect = camera.rect;

            float camX = mainCameraRect.x * Screen.width;
            float camY = mainCameraRect.y * Screen.height;

            float camWidth = mainCameraRect.width * Screen.width;
            float camHeight = mainCameraRect.height * Screen.height;

            float clampedX = Mathf.Clamp(mousePos.x, camX, camX + camWidth);
            float clampedY = Mathf.Clamp(mousePos.y, camY, camY + camHeight);

            float normalizedX = (clampedX - camX) / camWidth;
            float normalizedY = (clampedY - camY) / camHeight;

            float canvasWidth = canvas.rect.width;
            float canvasHeight = canvas.rect.height;

            float mappedX = (normalizedX - 0.5f) * canvasWidth;
            float mappedY = (normalizedY - 0.5f) * canvasHeight;
            
            return new(mappedX, mappedY);
        }
    }
}