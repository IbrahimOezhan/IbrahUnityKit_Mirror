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

        public List<Local_Key> Keys => keys;

        public float Steps => steps;

        public float DefaultValue => defaultValue;
        
        public Vector2 RangeFloat => rangeFloat;
        
        public Vector2 RangeInt => rangeInt;
        
        public DisplayType DisplayType => displayType;
        
        public bool Loop => loop;

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

        public bool IsInRange(float value)
        {
            switch(displayType)
            {
                case DisplayType.NUMBER:
                    return  value >= rangeFloat.x && value <= rangeFloat.y;
                case DisplayType.STRING:
                    return  value >= rangeInt.x && value <= rangeInt.y;
                case DisplayType.BOOL:
                    return value is >= 0 and <= 1;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Setting GetInstance(float value = float.NaN)
        {
            if (float.IsNaN(value) || !IsInRange(value)) value = defaultValue;

            return new Setting(key, rangeFloat, rangeInt, displayType, keys, loop, steps, value);
        }
    }

    public enum DisplayType
    {
        NUMBER,
        STRING,
        BOOL,
    }
}