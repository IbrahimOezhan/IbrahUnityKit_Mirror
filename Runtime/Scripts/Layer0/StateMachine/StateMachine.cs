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

        public TState RunMachine()
        {
            if (stack.Count == 0)
            {
                currentState?.StateExit();
                return null;
            }

            TState _currentState = stack.Peek();

            // Letzter state wurde gepopped
            if (_currentState != currentState)
            {
                currentState?.StateExit();
                stateChanged?.Invoke(currentState, _currentState);
                currentState = _currentState;
                currentState?.StateEnter();
            }

            TState nextState = _currentState.StateRun();

            // Entferne vom Stack wenn State Null zurückgibt. Der State entfernt sich selber vom stack
            if (nextState == null)
            {
                stack.Pop();
            }
            // Der State wird ersetzt durch einen neuen State
            else if (nextState != _currentState)
            {
                stack.Pop();
                stack.Push(nextState);
            }
            else if (nextState == _currentState)
            {
            }

            return _currentState;
        }

        public TState GetState() => currentState;
    }
}