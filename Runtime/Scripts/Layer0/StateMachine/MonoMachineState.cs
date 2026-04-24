#region

using UnityEngine;

#endregion

namespace IbrahKit.StateMachine
{
    public abstract class MonoMachineState<MState> : MonoBehaviour where MState : MonoMachineState<MState>
    {
        public abstract void StateEnter();

        public abstract MState StateRun();
        public abstract void StateExit();
    }
}