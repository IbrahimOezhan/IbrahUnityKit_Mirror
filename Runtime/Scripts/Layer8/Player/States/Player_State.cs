#region

using IbrahKit.StateMachine;
using UnityEngine;

#endregion

namespace IbrahKit.ThreeDPlayer
{
    [RequireComponent(typeof(Player_Controller))]
    public abstract class Player_State : MonoMachineState<Player_State>
    {
        protected Player_Controller controller;

        private void Awake()
        {
            controller = GetComponent<Player_Controller>();

            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }
    }
}