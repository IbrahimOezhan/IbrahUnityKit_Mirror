#region

using System;
using System.Text;
using UnityEngine;

#endregion

namespace IbrahKit.InfoCollector
{
    /// <summary>
    ///     An interface that provides methods for the debugmanager to use
    /// </summary>
    public interface IInfoCollector
    {
        GameObject gameObject { get; }

        /// <summary>
        ///     Returns the content to display in the debug menu
        /// </summary>
        /// <returns>The content to display in the debug menu</returns>
        public string GetDebugContent();

        /// <summary>
        ///     Returns the order in which the content must be displayed
        /// </summary>
        /// <returns>The order in which the content must be displayed</returns>
        public int GetDebugOrder();

        /// <summary>
        ///     Appends the DebugContent to the passed StringBuilder
        /// </summary>
        /// <param name="sb"></param>
        /// The StringBuilder to append to
        /// <param name="catchExceptions"></param>
        /// Whether to catch exceptions and append them or not
        public void Run(StringBuilder sb, bool catchExceptions)
        {
            if (!catchExceptions)
            {
                sb.AppendLine(GetDebugContent());
                return;
            }

            try
            {
                sb.AppendLine(GetDebugContent());
            }
            catch (Exception ex)
            {
                sb.AppendLine($"{gameObject.name} caused an exception: {ex.Message}");
            }
        }
    }
}