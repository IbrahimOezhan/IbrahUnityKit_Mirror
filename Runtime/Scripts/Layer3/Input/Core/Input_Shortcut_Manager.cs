#region

using System;
using System.Collections.Generic;
using IbrahKit.Debugging;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

#endregion

namespace IbrahKit.Input
{
    public class Input_Shortcut_Manager : Manager_Global<Input_Shortcut_Manager>
    {
        private readonly Dictionary<Key, List<Action>> keyValuePairs = new();

        private void Update()
        {
            foreach (var item in keyValuePairs)
            {
                KeyControl key = Keyboard.current[item.Key];

                if (key.wasPressedThisFrame)
                {
                    item.Value.ForEach(x => x.Invoke());
                }
            }
        }

        public void RegisterAction(Key key, Action ac)
        {
            if (key == Key.None)
            {
                IbrahDebug.LogWarning("Cannot add key None");
                return;
            }

            if (!keyValuePairs.ContainsKey(key))
            {
                keyValuePairs.Add(key, new List<Action>());
            }

            keyValuePairs[key].Add(ac);
        }

        public void UnregisterAction(Key key, Action ac)
        {
            if (!keyValuePairs.ContainsKey(key))
            {
                IbrahDebug.LogWarning($"Key {key} not registered. Therefore removal of action not possible");

                return;
            }

            keyValuePairs[key].Remove(ac);

            if (keyValuePairs[key].Count == 0) keyValuePairs.Remove(key);
        }
    }
}