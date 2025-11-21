using UnityEngine;

namespace IbrahKit
{
    public abstract class Extension_Handler_Base : MonoBehaviour
    {
        public abstract void InitExtensions();

        public abstract void RunExtensions();

        public abstract void Cleanup();
    }
}