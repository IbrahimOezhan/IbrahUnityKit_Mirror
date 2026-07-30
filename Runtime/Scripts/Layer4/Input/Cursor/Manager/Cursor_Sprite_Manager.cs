#region

using System.Collections.Generic;
using System.Linq;
using IbrahKit.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

#endregion

namespace IbrahKit.Input
{
    public class Cursor_Sprite_Manager : Manager_Global<Cursor_Sprite_Manager>
    {
        public enum CursorState
        {
            None,
            Hovering,
            Down,
        }

        [SerializeField] private Transform cursorTransform;

        [SerializeField] private RectTransform canvas;

        [SerializeField] private Cursor_Sprite_Style spriteStyle;

        [SerializeField] private Image cursorImage;

        private Camera camera;
        
        private CursorState state;

        private void Update()
        {
            if (!camera) camera = Camera.main;

            SetCursorState();

            SetCursorPos();

            spriteStyle.Set(cursorImage, state);
        }

        private void SetCursorState()
        {
            if(!Cursor_Input_Manager.TryGet(out Cursor_Input_Manager cim)) return;

            bool hovering = IsHovering(EventSystem.current, cim);

            if (hovering)
            {
                ButtonControl leftButton = Mouse.current.leftButton;

                if (leftButton.wasPressedThisFrame || (state == CursorState.Down && leftButton.isPressed))
                    SetState(CursorState.Down);
                
                else SetState(CursorState.Hovering);
            }
            else SetState(CursorState.None);
        }
        
        private void SetState(CursorState state)
        {
            this.state = state;
        }

        private void SetCursorPos()
        {
            if(!Cursor_Input_Manager.TryGet(out Cursor_Input_Manager cim)) return;
            
            Vector2 pos = GetCursorPos(cim.GetMousePos());

            cursorTransform.localPosition = pos;
        }

        private Vector2 GetCursorPos(Vector2 mousePos)
        {
            if (!camera || !canvas) return Vector2.zero;

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

        private bool IsHovering(EventSystem system, Cursor_Input_Manager cim)
        {
            if (!camera || !system) return false;
            
            return system.IsPointerOverGameObject() ? cim.CursorOverUI(system) :cim.CursorOverGameUI(camera);
        }
    }
}