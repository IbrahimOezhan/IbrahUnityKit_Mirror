#region

using System.Collections;

#endregion

namespace IbrahKit.Interaction
{
    public interface IInteractable
    {
        public void OnInteract(Interactable _interactable);

        public IEnumerator OnInteractRoutine(Interactable _interactable);
    }
}