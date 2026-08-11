#region

using System.Collections.Generic;
using System.Text;
using IbrahKit.Debugging;
using IbrahKit.Manager;
using IbrahKit.Utilities;
using Sirenix.Utilities;
using UnityEngine;

#endregion

namespace IbrahKit.InfoCollector
{
    /**
     * Manager class that collects information from other objects that implement the IInfoCollector interface.
     * This information can then be displayed on the screen for example as a sort of debug screen.
     */
    public class Info_Collection_Manager : MonoBehaviourSingletonDontDestroyOnLoad<Info_Collection_Manager>
    {
        private const string ERROR = "Info collector is null";

        [SerializeField] private bool catchExceptions = true;
        private readonly SortedDictionary<int, HashSet<IInfoCollector>> collectors = new();

        /// <summary>
        ///     Gets a formatted string from the information of all collectors
        /// </summary>
        /// <returns>A formatted string from the information of all collectors</returns>
        public string GetInfoString()
        {
            StringBuilder sb = new();

            collectors.ForEach(x =>
            {
                foreach (IInfoCollector infoCollector in x.Value)
                {
                    if (infoCollector == null)
                    {
                        IbrahDebug.LogError(ERROR);
                        sb.AppendLine(Color.darkRed.UseOnString(ERROR));
                    }
                    else infoCollector.Run(sb, catchExceptions);
                }
            });

            string s = sb.ToString();

            string content = string.IsNullOrWhiteSpace(s) || s == string.Empty ? "No Information" : s;

            return content;
        }

        /// <summary>
        ///     Adds a collector to the list
        /// </summary>
        /// <param name="infoCollector">The collector to add</param>
        public void RegisterInfoCollector(IInfoCollector infoCollector)
        {
            if (infoCollector == null)
            {
                IbrahDebug.LogError("Info Collector is null");
                return;
            }

            if (!collectors.TryGetValue(infoCollector.GetDebugOrder(), out HashSet<IInfoCollector> colls))
            {
                colls = new HashSet<IInfoCollector>();
                collectors.Add(infoCollector.GetDebugOrder(), colls);
            }

            colls.Add(infoCollector);
        }

        /// <summary>
        ///     Removes a collector from the list
        /// </summary>
        /// <param name="infoCollector">The collector to remove</param>
        public void UnregisterInfoCollector(IInfoCollector infoCollector)
        {
            if (infoCollector == null)
            {
                IbrahDebug.LogError("Info Collector is null");
                return;
            }

            if (!collectors.TryGetValue(infoCollector.GetDebugOrder(), out HashSet<IInfoCollector> colls))
            {
                IbrahDebug.LogError("Info Collector is not registered");
                return;
            }

            colls.Remove(infoCollector);

            if (colls.Count == 0) collectors.Remove(infoCollector.GetDebugOrder());
        }
    }
}