using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    public class Pause_Manager_Data : ScriptableObject
    {
        [SerializeField] private State_Key pausedState;

        [SerializeField] private List<AllowPause> allowPause = new();

        public State_Key GetPausedKey() => pausedState;

        public List<AllowPause> GetAllowPauses() => allowPause;
    }
}
