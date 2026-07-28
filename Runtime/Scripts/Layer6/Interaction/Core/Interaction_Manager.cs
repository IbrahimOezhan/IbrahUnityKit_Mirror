#region

using IbrahKit.Localization;
using IbrahKit.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.Interaction
{
    /// <summary>
    ///     Holds the State Machine for Interacting and the information on what the player.
    ///     Is not a Singleton Manager to allow adding 2 of them for local multiplayer games
    /// </summary>
    public abstract partial class Interaction_Manager : MonoBehaviour
    {
        [SerializeField] private Local_Key interactionKey;
        private StateMachine<InteractionMachineState> stateMachine;

        protected void RunStateMachine()
        {
            stateMachine ??= new(new InteractionStateNone(5, this));

            stateMachine.RunMachine();
        }

        public StateMachine<InteractionMachineState> GetStateMachine() => stateMachine;

        public Local_Key GetLocalKey() => interactionKey;

        protected abstract InputAction GetInteractInputAction();

        protected abstract Interactable FindInteractable();

        protected abstract bool CanInteract(Interactable i);
    }
}