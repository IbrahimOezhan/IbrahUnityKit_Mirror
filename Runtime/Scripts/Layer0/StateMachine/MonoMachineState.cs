#region

using UnityEngine;

#endregion

namespace IbrahKit.StateMachine
{
    public abstract class MonoMachineState<TMState> : MonoBehaviour where TMState : MonoMachineState<TMState>
    {
        public abstract void StateEnter();

        public abstract TMState StateRun();
        
        public abstract void StateExit();
    }
}