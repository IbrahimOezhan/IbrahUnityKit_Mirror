#nullable enable
using System;

namespace IbrahKit.StateMachine
{
    public interface IState<out TState> where TState : class?
    {
        public void StateEnter();

        public TState StateRun();
        
        public void StateExit();
    }
}