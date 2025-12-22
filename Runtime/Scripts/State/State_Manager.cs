using IbrahKit.Debugging;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [DefaultExecutionOrder(Execution_Order.state)]
    public class State_Manager : Manager_Global<State_Manager>, IDebug
    {
        public const string KEY = "States";

        [SerializeField, ReadOnly] private string currentState;

        [SerializeField, OnValueChanged(nameof(OnValueChanged))] private List<string> statesList = new();

        public event Action<string> OnStateChange;

        private void Start()
        {
            Lifecycle_Diagnostics_Manager.GetInstance().Add(this);
        }

        private void OnValueChanged()
        {
            Key_Database_Finder.TrySetKeys(KEY, statesList);
        }

        public void SetCurrentState(string newState)
        {
            if (statesList.Contains(newState))
            {
                currentState = newState;

                StateUpdate();
            }
            else
            {
                IbrahDebug.LogWarning($"State {newState} does not exist");
            }
        }

        public string GetCurrentState()
        {
            return currentState;
        }

        public void StateUpdate()
        {
            OnStateChange?.Invoke(GetCurrentState());
        }

        public bool CompareState(string state)
        {
            return currentState.Equals(state);
        }

        public string DebugContent()
        {
            return "Current State: " + currentState;
        }

        public int DebugOrder()
        {
            return -60;
        }
    }
}