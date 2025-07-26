using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace IbrahKit
{
    public class Debug_Manager : MonoBehaviour
    {
        private List<IDebug> debugs = new();

        [SerializeField] private UI_Text_Setter debugContent;

        [SerializeField] private GameObject debugContainer;

        [SerializeField] private KeyMap keyMap;

        public bool disableLogs;

        public static Debug_Manager Instance;

        public static bool bufferLogs;
        public static bool s_disableLogs;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Update()
        {
            if (Keyboard.current[keyMap.debugMenu].wasPressedThisFrame)
            {
                if (debugContainer.activeInHierarchy)
                {
                    debugContainer.SetActive(false);
                }
                else debugContainer.SetActive(true);
            }
            s_disableLogs = disableLogs;
        }

        private void FixedUpdate()
        {
            if (debugContainer.activeInHierarchy)
            {
                StringBuilder sb = new();

                foreach (var debug in debugs)
                {
                    sb.Append(debug.Run());
                    sb.AppendLine();
                }

                string s = sb.ToString();

                if (String_Utilities.IsEmpty(s))
                {
                    debugContent.SetText("No Information");
                }
                else debugContent.SetText(s);
            }
        }

        public void Add(IDebug debug)
        {
            debugs.Add(debug);
            debugs.Sort((a, b) =>
            {
                return a.Order().CompareTo(b.Order());
            });
        }

        public void Remove(IDebug debug)
        {
            debugs.Remove(debug);
        }
    }
}