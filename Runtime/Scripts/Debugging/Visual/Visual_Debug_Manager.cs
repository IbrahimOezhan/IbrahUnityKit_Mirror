using IbrahKit.UI;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IbrahKit.Debugging
{
    public class Visual_Debug_Manager : Manager_Global<Visual_Debug_Manager>
    {
        private readonly List<IDebug> debugs = new();

        private string content;

        private Action action;

        [SerializeField, Required] private UI_Menu debugContainer;

        [SerializeField, Required] private Key debugKey;

        [SerializeField] private bool catchExceptions = true;


        private void Start()
        {
            action = () => debugContainer.GetStateController().Toggle();

            Input_Shortcut_Manager.GetInstance().RegisterAction(debugKey, action);
        }

        private void FixedUpdate()
        {
            if (debugContainer.GetStateController().GetCompactState() != UI_Menu_Controller_State.StateCompact.ENABLED)
            {
                return;
            }

            StringBuilder sb = new();

            foreach (IDebug debug in debugs)
            {
                debug.Run(sb, catchExceptions);
            }

            string s = sb.ToString();

            content = s.IsEmpty() ? "No Information" : s;
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();

            Input_Shortcut_Manager.GetInstance().UnregisterAction(debugKey, action);
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