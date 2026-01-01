using IbrahKit.State;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit.Pause
{
    public class Pause_Manager_Data : ScriptableObject
    {
        [SerializeField] private State_Key pausedState;

        [SerializeField] private List<Pause_Allow> allowPause = new();

        public State_Key GetPausedKey() => pausedState;

        internal List<Pause_Allow> GetAllowPauses() => allowPause;
    }
}
