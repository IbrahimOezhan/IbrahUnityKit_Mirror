#region

using System;
using IbrahKit.Override;
using IbrahKit.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.ThreeDPlayer
{
    [Serializable]
    public class Player_Move_Handler : Player_Input_Handler
    {
        const float speedOffset = 0.1f;

        [SerializeField] private float speedChangeRate = 10.0f;

        [SerializeField] private float baseSpeed = 3;

        [SerializeField] private float moveSpeedMultiplier = 1;

        [SerializeField] private bool analogMovement;
        private Vector2 inputVector;

        private Override_Struct<float> speed;

        public override bool Init(Player3D_Input input)
        {
            base.Init(input);

            speed = new(baseSpeed, new OverrideReplace<float>());

            input.Player.Move.performed += OnMove;

            input.Player.Move.canceled += OnMove;

            input.Player.Move.started += OnMove;

            return true;
        }

        public override void Disable()
        {
            base.Disable();

            input.Player.Move.performed -= OnMove;

            input.Player.Move.canceled -= OnMove;

            input.Player.Move.started -= OnMove;
        }

        public void Move(Vector2 input, float verticalMovement, Player_Controller controller, float deltaTime)
        {
            float targetSpeed = GetTargetSpeed();

            Vector3 controllerVelocity = controller.GetController().velocity;

            float currentHorizontalSpeed = controllerVelocity.WithY(0).magnitude;

            float inputMagnitude = analogMovement ? input.magnitude : 1f;

            float
                finalSpeed =
                    GetTargetSpeed(); // GetFinalSpeed(currentHorizontalSpeed, targetSpeed, deltaTime, inputMagnitude);

            Vector3 movementVector = GetMovementVector(input, verticalMovement, controller, finalSpeed) * deltaTime;

            //Debug.Log(targetSpeed + " " + currentHorizontalSpeed +  " " + inputMagnitude + " " + finalSpeed +  " " + movementVector);

            controller.GetController().Move(movementVector);
        }

        protected virtual Vector3 GetMovementVector(Vector2 input, float verticalMovement, Player_Controller controller,
            float finalSpeed)
        {
            Vector3 movementVector = GetInputTimesSpeed(input, controller.transform, finalSpeed);

            movementVector.y = verticalMovement;

            return movementVector;
        }

        protected virtual float GetTargetSpeed()
        {
            return speed.GetValue() * moveSpeedMultiplier;
        }

        protected virtual float GetFinalSpeed(float currentHorizontalSpeed, float targetSpeed, float deltaTime,
            float inputMagnitude)
        {
            float finalSpeed;

            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                finalSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    deltaTime * speedChangeRate);

                finalSpeed = Mathf.Round(finalSpeed * 1000f) / 1000f;
            }
            else
            {
                finalSpeed = targetSpeed;
            }

            return finalSpeed;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if (locked) return;

            inputVector = context.ReadValue<Vector2>();
        }

        public Override_Struct<float> GetSpeed()
        {
            return speed;
        }

        public Vector2 GetInput()
        {
            return inputVector;
        }

        public float GetInputMagnitude(Vector2 moveVector, Transform transform, float speed)
        {
            float mag = GetInputTimesSpeed(moveVector, transform, speed).magnitude;

            return mag == 0 ? 3 : mag;
        }

        private Vector3 GetInputTimesSpeed(Vector2 moveVector, Transform transform, float speed)
        {
            if (moveVector == Vector2.zero) return Vector3.zero;

            return TwoDToThreeDVector(moveVector, transform).normalized * speed;
        }

        private Vector3 TwoDToThreeDVector(Vector3 input, Transform transform)
        {
            Vector3 worldInput =
                transform.right * input.x +
                transform.forward * input.y;

            return worldInput;
        }
    }
}