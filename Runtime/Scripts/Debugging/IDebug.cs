using UnityEngine;

namespace IbrahKit
{
    /// <summary>
    /// An interface that provides methods for the debugmanager to use
    /// </summary>
    public interface IDebug
    {
        GameObject gameObject { get; }

        /// <summary>
        /// Returns the content to display in the debug menu
        /// </summary>
        /// <returns>The content to display in the debug menu</returns>
        public string DebugContent();

        /// <summary>
        /// Returns the order in which the content must be displayed
        /// </summary>
        /// <returns>The order in which the content must be displayed</returns>
        public int DebugOrder();
    }
}