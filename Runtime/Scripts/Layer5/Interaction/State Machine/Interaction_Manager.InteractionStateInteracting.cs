#region

#endregion

namespace IbrahKit.Interaction
{
    public abstract partial class Interaction_Manager
    {
        public class InteractionStateInteracting : InteractionMachineState
        {
            private readonly Interactable interactable;
            private Interaction interaction;

            public InteractionStateInteracting(float cooldown, Interaction_Manager manager, Interactable interactable) :
                base(cooldown, manager)
            {
                this.interactable = interactable;
            }

            public override void StateEnter()
            {
                interaction = interactable.Interact(manager);
            }

            public override InteractionMachineState StateRun()
            {
                if (interaction.IsDone()) return new InteractionStateCooldown(cooldown, manager);

                return this;
            }

            public override void StateExit()
            {
            }

            public Interactable GetInteractable() => interactable;
        }
    }
}