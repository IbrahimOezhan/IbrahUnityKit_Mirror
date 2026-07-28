#region

using System;
using System.Collections.Generic;

#endregion

namespace IbrahKit.StateMachine
{
    public class StateMachine<TState> where TState : MachineState<TState>
    {
        private TState currentState;

        private Stack<TState> stack;

        public Action<TState, TState> stateChanged;

        public StateMachine(TState state)
        {
            stack = new();
            Push(state);
        }

        public void Push(TState state)
        {
            if (state == null)
            {
                return;
            }

            stack.Push(state);
        }

        public void RunMachine()
        {
            if (stack.Count == 0)
            {
                return;
            }

            TState currentState = stack.Peek();

            // Letzter state wurde gepopped
            if (currentState != this.currentState)
            {
                this.currentState?.StateExit();
                stateChanged?.Invoke(this.currentState, currentState);
                this.currentState = currentState;
                this.currentState?.StateEnter();
            }

            TState nextState = currentState.StateRun();

            // Entferne vom Stack wenn State Null zurückgibt. Der State entfernt sich selber vom stack
            if (nextState == null)
            {
                stack.Pop();
            }
            // Der State wird ersetzt durch einen neuen State
            else if (nextState != currentState)
            {
                stack.Pop();
                stack.Push(nextState);
            }
            else if (nextState == currentState)
            {
            }
        }

        public TState GetState() => currentState;
    }
}