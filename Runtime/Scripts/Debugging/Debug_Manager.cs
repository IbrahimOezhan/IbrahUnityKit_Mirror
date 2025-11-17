using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IbrahKit
{
    public class Debug_Manager : Manager_DDOL<Debug_Manager>
    {
        private readonly List<IDebug> debugs = new();

        [SerializeField, Required] private UI_Text_Setter debugContent;

        [SerializeField, Required] private UI_Menu debugContainer;

        [SerializeField, Required] private KeyMap keyMap;

        [SerializeField] private bool catchExceptions = true;

        public bool disableLogs;

        public static bool bufferLogs;

        public static bool s_disableLogs;

        private void Update()
        {
            if (Keyboard.current[keyMap.debugMenu].wasPressedThisFrame)
            {
                debugContainer.GetStateController().Toggle();
            }

            s_disableLogs = disableLogs;
        }

        private void FixedUpdate()
        {
            if (debugContainer.GetStateController().GetCompactState() != Menu_State_Controller.StateCompact.ENABLED)
            {
                return;
            }

            StringBuilder sb = new();

            foreach (IDebug debug in debugs)
            {
                if (!catchExceptions)
                {
                    sb.AppendLine(debug.DebugContent());
                    continue;
                }
                try
                {
                    sb.AppendLine(debug.DebugContent());
                }
                catch (Exception ex)
                {
                    sb.AppendLine($"{debug.gameObject.name} caused an exception: {ex.Message}");
                }
            }

            string s = sb.ToString();

            debugContent.SetText(s.IsEmpty() ? "No Information" : s);
        }

        /// <summary>
        /// Adds a debug to the list
        /// </summary>
        /// <param name="debug">The debug to add</param>
        public void Add(IDebug debug)
        {
            debugs.Add(debug);
            debugs.Sort((a, b) =>
            {
                return a.DebugOrder().CompareTo(b.DebugOrder());
            });
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