#region

using System.Collections;

#endregion

namespace IbrahKit.Interaction
{
    /// <summary>
    ///     Interface for defining a method to call on interaction
    /// </summary>
    public interface IInteractable
    {
        public void OnInteract(Interactable _interactable);

        public IEnumerator OnInteractRoutine(Interactable _interactable);
    }
}