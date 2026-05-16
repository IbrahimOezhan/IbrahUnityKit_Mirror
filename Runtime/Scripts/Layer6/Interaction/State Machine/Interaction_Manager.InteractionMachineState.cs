#region

using IbrahKit.StateMachine;

#endregion

namespace IbrahKit.Interaction
{

    public abstract partial class Interaction_Manager
    {
        public abstract class InteractionMachineState : MachineState<InteractionMachineState>
        {
            protected float cooldown;

            protected readonly Interaction_Manager manager;

            protected InteractionMachineState(float cooldown, Interaction_Manager manager)
            {
                this.cooldown = cooldown;
                this.manager = manager;
            }
        }
    }

}