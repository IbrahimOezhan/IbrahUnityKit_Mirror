#region

using System;
using UnityEngine;

#endregion

namespace IbrahKit.ThreeDPlayer
{
    [Serializable]
    public class Player_Gravity_Handler : Player_Input_Handler
    {
        private Collider[] colliders;

        private bool grounded = false;

        private float verticalVelocity;

        [SerializeField] private float terminalVelocity;

        [SerializeField] private float gravity;

        [SerializeField] private float groundCheckOffset;

        [SerializeField] private float sphereRadiusOffset = -0.05f;

        [SerializeField] private LayerMask groundCheckMask;

        public void Update(Player_Controller controller, float deltaTime)
        {
            GroundedCheck(controller.transform, controller);

            if (grounded && terminalVelocity < 0.0f)
            {
                // Keep player grounded (small downward force to stick to ground)
                verticalVelocity = -2f;
            }
            else
            {
                if (verticalVelocity < terminalVelocity)
                {
                    verticalVelocity += gravity * deltaTime;
                }
            }
        }

        private void GroundedCheck(Transform transform, Player_Controller controller)
        {
            Vector3 spherePosition = new(transform.position.x, transform.position.y - groundCheckOffset,
                transform.position.z);

            colliders = Physics.OverlapSphere(spherePosition, controller.GetController().radius - sphereRadiusOffset,
                groundCheckMask, QueryTriggerInteraction.Ignore);

            grounded = colliders.Length > 0;
        }

        public void SetVerticalVelocity(float verticalVelocity)
        {
            this.verticalVelocity = verticalVelocity;
        }

        public float GetVerticalVelocity()
        {
            return verticalVelocity;
        }

        public bool IsGrounded()
        {
            return grounded;
        }
    }
}