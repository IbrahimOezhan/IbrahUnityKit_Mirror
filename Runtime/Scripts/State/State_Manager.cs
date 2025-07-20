using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [DefaultExecutionOrder(Execution_Order.state)]
    public class State_Manager : Manager_Base
    {
        public const string KEY = "States";

        [SerializeField, ReadOnly] private int currentState = 0;

        [SerializeField] private List<string> statesList = new();

        public event Action<string> OnStateChange;

        public static State_Manager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(gameObject);
            else
            {
                Instance = this;
            }
        }

        private void OnValidate()
        {
            String_Utilities.CreateDropdown(statesList, KEY);
        }

        public void SetCurrentState(string newState)
        {
            SetCurrentState(statesList.IndexOf(statesList.Find(x => x == newState)));
        }

        public void SetCurrentState(int index)
        {
            currentState = index;

            StateUpdate();
        }

        public string GetCurrentState()
        {
            return statesList[currentState];
        }

        public void StateUpdate()
        {
            OnStateChange?.Invoke(statesList[currentState]);
        }

        public bool CompareState(string state)
        {
            if (currentState >= statesList.Count) return false;

            return statesList[currentState].Equals(state);
        }
    }
}