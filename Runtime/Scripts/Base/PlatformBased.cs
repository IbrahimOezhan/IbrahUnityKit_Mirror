using UnityEngine;

namespace IbrahKit
{
    public abstract class PlatformBased
    {
        [SerializeField] private RuntimePlatform platform;

        public bool IsPlatform()
        {
            return Application.platform == platform;
        }
    }
}