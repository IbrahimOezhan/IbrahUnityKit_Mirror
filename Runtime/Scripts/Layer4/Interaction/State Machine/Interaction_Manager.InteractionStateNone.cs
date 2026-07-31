#region

using UnityEngine.InputSystem;

#endregion

namespace IbrahKit.Interaction
{
    public abstract partial class Interaction_Manager
    {
        public class InteractionStateNone : InteractionMachineState
        {
            private InputAction action;

            private Interactable i;

            public InteractionStateNone(float cooldown, Interaction_Manager manager) : base(cooldown, manager)
            {
            }

            public override void StateEnter()
            {
                action = manager.GetInteractInputAction();
            }

            public override InteractionMachineState StateRun()
            {
                i = manager.FindInteractable();

                if (manager.CanInteract(i) && action.WasPressedThisFrame())
                    return new InteractionStateInteracting(cooldown, manager, i);

                return this;
            }

            public override void StateExit()
            {
            }

            public Interactable GetInteractable() => i;
        }
    }
}