#nullable enable
using UnityEngine;

namespace IbrahKit.StateMachine
{
    //TODO: Proper exception throwing for null reference
    
    public class MonoBehaviour_StateMachine<TState> : MonoBehaviour where TState : class?, IState<TState>
    {
        private StateMachine<TState>? machine;

        public void Setup(TState state)
        {
            machine = new StateMachine<TState>(state);
        }

        public void RunMachine()
        {
            machine?.RunMachine();
        }

        public TState GetState() => machine?.GetState();
    }
}