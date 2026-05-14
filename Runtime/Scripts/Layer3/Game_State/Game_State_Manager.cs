#region

using IbrahKit.Core;
using IbrahKit.InfoCollector;
using IbrahKit.Manager;
using IbrahKit.StateMachine;
using UnityEngine;

#endregion

namespace IbrahKit.State
{
    [DefaultExecutionOrder(Execution_Order.state)]
    public class Game_State_Manager : Manager_Global<Game_State_Manager>, IInfoCollector
    {
        private StateMachine<Game_State> stateMachine;

        private void Start()
        {
            Info_Collection_Manager.GetInstance().RegisterInfoCollector(this);
        }

        public void StartStateMachine(Game_State initialState)
        {
            stateMachine = new(initialState);
        }

        private void Update()
        {
            if (stateMachine == null) return;
            
            stateMachine.RunMachine();
        }

        public StateMachine<Game_State> GetStateMachine() => stateMachine;

        public string GetDebugContent()
        {
            return "";
        }

        public int GetDebugOrder()
        {
            return -60;
        }
    }
}