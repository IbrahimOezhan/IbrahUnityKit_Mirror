#region

using System;
using System.Collections.Generic;
using IbrahKit.Core;
using IbrahKit.Debugging;
using IbrahKit.InfoCollector;
using IbrahKit.Keys;
using IbrahKit.Manager;
using IbrahKit.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.State
{
    [DefaultExecutionOrder(Execution_Order.state)]
    public class Game_State_Manager : Manager_Global<Game_State_Manager>, IDebug
    {
        private StateMachine<Game_State> stateMachine;

        private void Start()
        {
            Lifecycle_Diagnostics_Manager.GetInstance().Add(this);
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

        public string DebugContent()
        {
            return "";
        }

        public int DebugOrder()
        {
            return -60;
        }
    }
}