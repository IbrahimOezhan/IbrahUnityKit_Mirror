#region

using System;

#endregion

namespace IbrahKit.ThreeDPlayer
{
    [Serializable]
    public abstract class Player_Input_Handler
    {
        protected bool locked = false;

        protected Player3D_Input input;

        public virtual bool Init(Player3D_Input input)
        {
            this.input = input;

            return true;
        }

        public virtual void Disable()
        {
        }

        public virtual void Lock()
        {
            locked = true;
        }

        public virtual void Unlock()
        {
            locked = false;
        }
    }
}