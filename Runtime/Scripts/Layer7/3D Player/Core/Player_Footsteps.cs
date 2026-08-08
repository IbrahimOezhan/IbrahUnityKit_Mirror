#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.ThreeDPlayer
{
    public abstract class Player_Footsteps
    {
        private bool isGrounded;
        private Vector3 lastPosition;
        private float lastStepTime = 0f;
        private Vector2 movementVector;
        [SerializeField] private float stepsTriggerDistance = 1.25f;
        [SerializeField] private float timeBetweenSteps;
        private float totalDistanceTraveled = 0f;

        private void Execute(CharacterController controller, GameObject gameObject)
        {
            Vector3 _currentPos = gameObject.transform.position;

            float _distanceDelta = Vector3.Distance(_currentPos, lastPosition);

            if (_distanceDelta == 0) return;

            totalDistanceTraveled += _distanceDelta;

            float _currentSpeed = MathF.Round(controller.velocity.magnitude, 3);

            if (movementVector == Vector2.zero || !isGrounded) return;

            float _time = Time.time;
            float _timeSinceLastStep = _time - lastStepTime;

            if (totalDistanceTraveled >= stepsTriggerDistance || _timeSinceLastStep >= timeBetweenSteps)
            {
                gameObject.SendMessage("PlayFootstep", _currentSpeed);
                totalDistanceTraveled = 0f;
                lastStepTime = _time;
            }

            lastPosition = _currentPos;
        }

        public void SetIsGrounded(bool _isGrounded)
        {
            isGrounded = _isGrounded;
        }

        public void SetInput(Vector2 input)
        {
            movementVector = input;
        }
    }
}