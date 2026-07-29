#region

using System;
using System.Text;
using UnityEngine;

#endregion

namespace IbrahKit.InfoCollector
{
    /// <summary>
    ///     An interface that provides methods for the Info_Collection_Manager to use
    /// </summary>
    public interface IInfoCollector
    {
        GameObject gameObject { get; }

        /// <summary>
        ///     Returns information
        /// </summary>
        /// <returns>Information</returns>
        public string GetInformation();

        /// <summary>
        ///     Returns the order in which the content must be aggregated
        /// </summary>
        /// <returns>The order in which the content must be aggregated</returns>
        public int GetDebugOrder();

        /// <summary>
        ///     Appends the Information to the passed StringBuilder
        /// </summary>
        /// <param name="sb"></param>
        /// The StringBuilder to append to
        /// <param name="catchExceptions"></param>
        /// Whether to catch exceptions and append them or not
        public void Run(StringBuilder sb, bool catchExceptions)
        {
            if (!catchExceptions)
            {
                sb.AppendLine(GetInformation());
                return;
            }

            try
            {
                sb.AppendLine(GetInformation());
            }
            catch (Exception ex)
            {
                sb.AppendLine($"{gameObject.name} caused an exception: {ex.Message}");
            }
        }
    }
}