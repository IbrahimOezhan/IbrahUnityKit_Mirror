#region

using UnityEngine;

#endregion

namespace IbrahKit.Extension
{
    /// <summary>
    /// Class that holds extensions and provides methods to interact with them
    /// </summary>
    public abstract class Extension_Handler_Base : MonoBehaviour
    {
        public abstract void InitExtensions();

        public abstract void RunExtensions();

        public abstract void Cleanup();
    }
}