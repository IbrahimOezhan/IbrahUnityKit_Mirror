#region

using UnityEngine;

#endregion

namespace IbrahKit.StateMachine
{
    public abstract class MonoBehaviour_State<TMState> : MonoBehaviour, IState<TMState> where TMState : MonoBehaviour_State<TMState>
    {
        public abstract void StateEnter();

        public abstract TMState StateRun();

        public abstract void StateExit();
    }
}