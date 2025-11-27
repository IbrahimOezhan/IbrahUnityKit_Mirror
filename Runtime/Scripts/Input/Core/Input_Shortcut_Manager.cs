using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace IbrahKit
{
    public class Input_Shortcut_Manager : Manager_DDOL<Input_Shortcut_Manager>
    {
        private readonly Dictionary<Key, List<Action>> keyValuePairs = new();

        private void Update()
        {
            foreach (var item in keyValuePairs)
            {
                if (Keyboard.current[item.Key].wasPressedThisFrame)
                {
                    item.Value.ForEach(x => x.Invoke());
                }
            }
        }

        public void RegisterAction(Key key, Action ac)
        {
            if (keyValuePairs.ContainsKey(key))
            {
                keyValuePairs[key].Add(ac);
            }
            else
            {
                keyValuePairs[key] = new List<Action>
                {
                    ac
                };
            }
        }

        public void UnregisterAction(Key key, Action ac)
        {
            keyValuePairs[key].Remove(ac);

            if (keyValuePairs[key].Count == 0) keyValuePairs.Remove(key);
        }
    }
}
