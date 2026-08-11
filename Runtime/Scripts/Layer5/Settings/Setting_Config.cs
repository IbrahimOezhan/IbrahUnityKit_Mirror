#region

using System;
using System.Collections.Generic;
using IbrahKit.Keys;
using IbrahKit.Localization;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.Settings
{
    /// <summary>
    ///     Contains information on the type of the setting and the key it uses
    /// </summary>
    /// <typeparam name="TSetting"></typeparam>
    /// The parent class of which the type can be
    public abstract class Setting_Config : ScriptableObject, IKey
    {
        [SerializeField] private bool loop;

        [SerializeField] private string key;

        [SerializeField] private DisplayType displayType;

        [SerializeField, ShowIf(nameof(displayType), DisplayType.NUMBER)]
        private Vector2 rangeFloat;

        [SerializeField, ShowIf(nameof(displayType), DisplayType.STRING)]
        private Vector2Int rangeInt;

        [SerializeField] private float defaultValue;

        [SerializeField, ShowIf(nameof(displayType), DisplayType.NUMBER)]
        private float steps;

        [SerializeField, ShowIf(nameof(displayType), DisplayType.STRING)]
        private List<Local_Key> keys = new();

        public string GetKey()
        {
            return key;
        }

        private void OnValueChanged()
        {
            switch (displayType)
            {
                case DisplayType.NUMBER:
                    while (keys.Count < rangeFloat.y)
                    {
                        keys.Add(new Local_Key());
                    }

                    while (keys.Count > rangeFloat.y)
                    {
                        keys.RemoveAt(keys.Count - 1);
                    }

                    break;
                case DisplayType.STRING:
                    while (keys.Count < rangeInt.y)
                    {
                        keys.Add(new Local_Key());
                    }

                    while (keys.Count > rangeInt.y)
                    {
                        keys.RemoveAt(keys.Count - 1);
                    }

                    break;
                case DisplayType.BOOL:
                    while (keys.Count < 2)
                    {
                        keys.Add(new Local_Key());
                    }

                    while (keys.Count > 2)
                    {
                        keys.RemoveAt(keys.Count - 1);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Setting GetInstance()
        {
            return new Setting(key, rangeFloat, rangeInt, displayType, keys, loop, steps, defaultValue);
        }
    }

    public enum DisplayType
    {
        NUMBER,
        STRING,
        BOOL,
    }
}