using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [DefaultExecutionOrder(Execution_Order.state)]
    public class State_Manager : Manager_DDOL<State_Manager>, IDebug
    {
        public const string KEY = "States";

        [SerializeField, ReadOnly] private string currentState;

        [SerializeField] private List<string> statesList = new();

        public event Action<string> OnStateChange;

        private void Start()
        {
            Debug_Manager.GetInstance().Add(this);
        }

        private void OnValidate()
        {
            Dropdown_Utilities.CreateDropdown(statesList, KEY);
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
                Debug.LogWarning($"State {newState} does not exist");
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