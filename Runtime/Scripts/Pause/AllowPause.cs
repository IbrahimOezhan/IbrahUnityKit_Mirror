using System;
using UnityEngine;

namespace IbrahKit
{
    [Serializable]
    public class AllowPause
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