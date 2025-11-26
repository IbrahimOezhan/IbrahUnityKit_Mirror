using IbrahKit.UI;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IbrahKit.Debug
{
    public class Debug_Manager : Manager_DDOL<Debug_Manager>, ISelfValidator
    {
        private readonly List<IDebug> debugs = new();

        private UI_Interative_Extension_Text_Setter textSetter;

        [SerializeField] private UI_Interactive debugContent;

        [SerializeField, Required] private UI_Menu debugContainer;

        [SerializeField, Required] private KeyMap keyMap;

        [SerializeField] private bool catchExceptions = true;

        public bool disableLogs;

        public static bool bufferLogs;

        public static bool s_disableLogs;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();

            debugContent.TryGet(out textSetter);
        }

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

            textSetter.SetText(s.IsEmpty() ? "No Information" : s);
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

        public void Validate(SelfValidationResult result)
        {
            if (debugContent == null)
            {
                result.AddError("Debug Content is null");
                return;
            }

            if (!debugContent.TryGet(out textSetter))
            {
                result.AddError("UI Interactive doesnt contain Text Setter");
            }
        }
    }
}