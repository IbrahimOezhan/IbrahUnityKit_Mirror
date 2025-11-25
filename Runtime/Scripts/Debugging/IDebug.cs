using System;
using System.Text;
using UnityEngine;

namespace IbrahKit.Debug
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

        public void Run(StringBuilder sb, bool catchExceptions)
        {
            if (!catchExceptions)
            {
                sb.AppendLine(DebugContent());
                return;
            }
            try
            {
                sb.AppendLine(DebugContent());
            }
            catch (Exception ex)
            {
                sb.AppendLine($"{gameObject.name} caused an exception: {ex.Message}");
            }
        }
    }
}