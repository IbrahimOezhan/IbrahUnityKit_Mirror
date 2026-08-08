#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.ThreeDPlayer
{
    [Serializable]
    public class Player_Look_Handler : Player_Input_Handler
    {
        private const string MOUSE = "Mouse";

        [SerializeField, ReadOnly] private bool isUsingMouse;

        [SerializeField, HideLabel, InlineProperty]
        private First_Person_Look fpLook;

        private Vector2 lookVector;

        public override bool Init(Player3D_Input input)
        {
            base.Init(input);

            input.Player.Look.performed += OnLook;

            input.Player.Look.canceled += OnLook;

            input.Player.Look.started += OnLook;

            fpLook.Init();

            return true;
        }

        public override void Disable()
        {
            base.Disable();

            input.Player.Look.performed -= OnLook;

            input.Player.Look.canceled -= OnLook;

            input.Player.Look.started -= OnLook;
        }

        public void Look(Vector2 input, Player_Controller controller, float deltaTime)
        {
            fpLook.Look(input, controller.transform, isUsingMouse ? 1.0f : deltaTime);
        }

        public void SyncFromTransforms(Transform cameraTarget)
        {
            fpLook.SyncFromTransforms(cameraTarget);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (locked) return;

            isUsingMouse = context.control.ToString().Contains(MOUSE);

            lookVector = context.ReadValue<Vector2>();
        }

        public Vector2 GetInput()
        {
            return lookVector;
        }
    }
}