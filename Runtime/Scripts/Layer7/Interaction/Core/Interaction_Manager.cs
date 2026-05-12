#region

using System.Collections.Generic;
using IbrahKit.Localization;
using IbrahKit.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.Interaction
{
    public abstract partial class Interaction_Manager : MonoBehaviour
    {
        private StateMachine<InteractionMachineState> stateMachine;

        private List<Interactable> interacting = new();

        [SerializeField] private Local_Key interactionKey;

        protected void RunStateMachine()
        {
            if (stateMachine == null) stateMachine = new(new InteractionStateNone(5, this));

            stateMachine.RunMachine();
        }

        public void Register(Interactable i)
        {
            interacting.Add(i);
        }

        public void Unregister(Interactable i)
        {
            interacting.Remove(i);
        }

        public StateMachine<InteractionMachineState> GetStateMachine() => stateMachine;

        protected bool IsInteracting()
        {
            return interacting.Count != 0;
        }

        protected abstract InputAction GetInteractInputAction();

        protected abstract Interactable FindInteractable();

        protected abstract bool CanInteract(Interactable i);

        public Local_Key GetLocalKey() => interactionKey;
    }
}
