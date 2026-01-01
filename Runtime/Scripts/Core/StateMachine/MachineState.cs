using UnityEngine;

namespace IbrahKit
{
    public abstract class MachineState<MState>  where MState : MachineState<MState>
    {
        public abstract void StateEnter();

        public abstract MState StateRun();
        public abstract void StateExit();
    }
}
