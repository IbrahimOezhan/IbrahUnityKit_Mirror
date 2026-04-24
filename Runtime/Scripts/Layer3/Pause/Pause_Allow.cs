#region

using System;
using IbrahKit.State;
using UnityEngine;

#endregion

namespace IbrahKit.Pause
{
    [Serializable]
    internal class Pause_Allow
    {
        [SerializeField] private bool allow;

        [SerializeField] private State_Key state;

        public bool Allow()
        {
            return allow;
        }

        public bool IsState(string state)
        {
            return state.Equals(state);
        }
    }
}