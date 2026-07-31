#region

using UnityEngine;

#endregion

namespace IbrahKit.Interaction
{
    public abstract partial class Interaction_Manager
    {
        public class InteractionStateCooldown : InteractionMachineState
        {
            public InteractionStateCooldown(float cooldown, Interaction_Manager manager) : base(cooldown, manager)
            {
            }

            public override void StateEnter()
            {
            }

            public override InteractionMachineState StateRun()
            {
                cooldown -= Time.deltaTime;

                if (cooldown <= 0)
                {
                    return new InteractionStateNone(cooldown, manager);
                }

                return this;
            }

            public override void StateExit()
            {
            }
        }
    }
}