#region

using IbrahKit.Collision;
using IbrahKit.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.Interaction
{
    public class Interactable_Collider : Interactable, ITrigger
    {
        private readonly Color arrowColor = Color.white;

        [SerializeField] private Transform player;

        [SerializeField] private SpriteRenderer sprite;

        [SerializeField] private float fadeOutDistance;

        [SerializeField] private float fadeInDistance;

        private void FixedUpdate()
        {
            float _distance = Vector3.Distance(player.position, transform.position);

            sprite.color = arrowColor.WithAlpha(_distance < fadeOutDistance ? _distance - fadeOutDistance : (_distance > fadeInDistance ? fadeInDistance - _distance : 255));
        }
    }
}
