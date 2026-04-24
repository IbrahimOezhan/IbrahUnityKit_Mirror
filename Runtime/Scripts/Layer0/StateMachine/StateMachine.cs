namespace IbrahKit.StateMachine
{
    public class StateMachine<TState> where TState : MachineState<TState>
    {
        private TState currentState;

        public StateMachine(TState state)
        {
            SetState(state);
        }

        public void RunMachine()
        {
            TState resolvedState = currentState.StateRun();

            if (resolvedState != currentState) SetState(resolvedState);
        }

        private void SetState(TState state)
        {
            currentState?.StateExit();

            currentState = state;

            currentState?.StateEnter();
        }

        public TState GetState() => currentState;
    }
}