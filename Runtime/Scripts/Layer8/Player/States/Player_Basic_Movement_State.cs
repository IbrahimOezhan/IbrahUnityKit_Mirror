#region

using UnityEngine;

#endregion

namespace IbrahKit.ThreeDPlayer
{
    public class Player_Basic_Movement_State : Player_State
    {
        private Player_Move_Handler moveHandler;

        private Player_Look_Handler lookHandler;

        private Player_Gravity_Handler gravityHandler;

        private void Start()
        {
            moveHandler = controller.GetHandler<Player_Move_Handler>();

            lookHandler = controller.GetHandler<Player_Look_Handler>();

            gravityHandler = controller.GetHandler<Player_Gravity_Handler>();
        }

        public override void StateEnter()
        {
        }

        public override Player_State StateRun()
        {
            moveHandler.Move(moveHandler.GetInput(), gravityHandler.GetVerticalVelocity(), controller, Time.deltaTime);

            lookHandler.Look(lookHandler.GetInput(), controller, Time.deltaTime);

            return this;
        }

        public override void StateExit()
        {
        }
    }
}