namespace IbrahKit.Interaction
{
    public class Interactable_PlacePickup : Interactable
    {
        public override bool CanInteract()
        {
            return Player_Pickup.Instance.IsHandFull();
        }
    }
}