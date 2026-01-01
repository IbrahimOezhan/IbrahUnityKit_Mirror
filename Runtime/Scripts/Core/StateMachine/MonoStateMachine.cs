namespace IbrahKit
{
    public class MonoStateMachine<TState> where TState : MonoMachineState<TState>
    {
        private TState currentState;

        public MonoStateMachine(TState state)
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
