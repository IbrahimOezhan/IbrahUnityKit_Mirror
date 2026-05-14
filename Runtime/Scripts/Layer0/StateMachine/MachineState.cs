namespace IbrahKit.StateMachine
{
    public abstract class MachineState<TMState> where TMState : MachineState<TMState>
    {
        public abstract void StateEnter();

        public abstract TMState StateRun();
        
        public abstract void StateExit();
    }
}