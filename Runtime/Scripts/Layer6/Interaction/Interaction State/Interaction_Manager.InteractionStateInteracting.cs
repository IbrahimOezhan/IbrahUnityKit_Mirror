#region

#endregion

namespace IbrahKit.Interaction
{
    public abstract partial class Interaction_Manager
    {
        public class InteractionStateInteracting : InteractionMachineState
        {
            private Interactable i;
            private Interaction interaction;

            public InteractionStateInteracting(float cooldown, Interaction_Manager manager, Interactable i) : base(
                cooldown, manager)
            {
                this.i = i;
            }

            public override void StateEnter()
            {
                interaction = i.Interact(manager);
            }

            public override InteractionMachineState StateRun()
            {
                if (interaction.IsDone()) return new InteractionStateCooldown(cooldown, manager);

                return this;
            }

            public override void StateExit()
            {

            }

            public Interactable GetInteractable() => i;
        }
    }
}