#region

using IbrahKit.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

#endregion

public class Cursor_Custom_Manager : Cursor_State_Manager
{
    public enum CursorClickState
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

    private CursorClickState state;

    public override void Disabled()
    {
        cursorImage.gameObject.SetActive(false);
    }

    public override void Clamped()
    {
        cursorImage.gameObject.SetActive(true);
        RenderCursor();
    }

    public override void Unclamped()
    {
        cursorImage.gameObject.SetActive(true);
        RenderCursor();
    }

    private void RenderCursor()
    {
        if (!camera) camera = Camera.main;

        SetCursorState();

        SetCursorPos();

        spriteStyle.Set(cursorImage, state);
    }

    private void SetCursorState()
    {
        if (!Cursor_Input_Manager.TryGet(out Cursor_Input_Manager cim)) return;

        bool hovering = IsHovering(EventSystem.current, cim);

        if (hovering)
        {
            ButtonControl leftButton = Mouse.current.leftButton;

            if (leftButton.wasPressedThisFrame || (state == CursorClickState.Down && leftButton.isPressed))
                SetState(CursorClickState.Down);

            else SetState(CursorClickState.Hovering);
        }
        else SetState(CursorClickState.None);
    }

    private void SetState(CursorClickState state)
    {
        this.state = state;
    }

    private void SetCursorPos()
    {
        if (!Cursor_Input_Manager.TryGet(out Cursor_Input_Manager cim)) return;

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

        return system.IsPointerOverGameObject() ? cim.CursorOverUI(system) : cim.CursorOverGameUI(camera);
    }
}