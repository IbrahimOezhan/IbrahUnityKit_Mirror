#region

using System;
using IbrahKit.Keys;
using IbrahKit.StateMachine;

#endregion

namespace IbrahKit.State
{
    [Serializable]
    public abstract class Game_State : MachineState<Game_State>
    {
        public abstract bool ShowCursor();

        public abstract bool CanPause();
    }
}