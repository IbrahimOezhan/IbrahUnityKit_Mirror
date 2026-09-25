#nullable enable
namespace IbrahKit.StateMachine
{
    public abstract class State<TMState> : IState<TMState> where TMState : class?
    {
        public abstract void StateEnter();

        public abstract TMState StateRun();

        public abstract void StateExit();
    }
}