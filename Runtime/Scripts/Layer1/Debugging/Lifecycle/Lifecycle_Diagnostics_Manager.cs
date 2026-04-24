#region

using System.Collections.Generic;
using System.Text;
using UnityEngine;

#endregion

namespace IbrahKit.Debugging
{
    public class Lifecycle_Diagnostics_Manager : Manager_Global<Lifecycle_Diagnostics_Manager>
    {
        private readonly List<IDebug> debugs = new();

        [SerializeField] private bool catchExceptions = true;

        public string GetLifecycleContent()
        {
            StringBuilder sb = new();

            foreach (IDebug debug in debugs)
            {
                debug.Run(sb, catchExceptions);
            }

            string s = sb.ToString();

            string content = string.IsNullOrWhiteSpace(s) || s == string.Empty ? "No Information" : s;

            return content;
        }

        /// <summary>
        /// Adds a debug to the list
        /// </summary>
        /// <param name="debug">The debug to add</param>
        public void Add(IDebug debug)
        {
            debugs.Add(debug);
            debugs.Sort((a, b) => { return a.DebugOrder().CompareTo(b.DebugOrder()); });
        }

        /// <summary>
        /// Removes a debug from the list
        /// </summary>
        /// <param name="debug">The debug to remove</param>
        public void Remove(IDebug debug)
        {
            debugs.Remove(debug);
        }
    }
}