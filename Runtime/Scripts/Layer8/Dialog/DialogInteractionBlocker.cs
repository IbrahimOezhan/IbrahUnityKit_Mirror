#region

using IbrahKit.Interaction;

#endregion

namespace IbrahKit.Dialog
{
    public class DialogInteractionBlocker : IInteractionBlocker
    {
        public bool Block()
        {
            return Dialog_Manager.dialog != null;
        }
    }
}