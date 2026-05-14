#region

using System.Collections.Generic;
using IbrahKit.Debugging;
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

        private readonly List<Interactable> interacting = new();

        [SerializeField] private Local_Key interactionKey;

        protected void RunStateMachine()
        {
            if (stateMachine == null) stateMachine = new(new InteractionStateNone(5, this));

            stateMachine.RunMachine();
        }

        public void Register(Interactable i)
        {
            if (i == null)
            {
                IbrahDebug.LogError("Interactable is null");
                return;
            }
            
            interacting.Add(i);
        }

        public void Unregister(Interactable i)
        {
            if (i == null)
            {
                IbrahDebug.LogError("Interactable is null");
                return;
            }
            
            interacting.Remove(i);
        }

        public StateMachine<InteractionMachineState> GetStateMachine() => stateMachine;

        public bool IsInteracting() => interacting.Count != 0;

        public Local_Key GetLocalKey() => interactionKey;
        
        protected abstract InputAction GetInteractInputAction();

        protected abstract Interactable FindInteractable();

        protected abstract bool CanInteract(Interactable i);
    }
}
