#region

using System;
using System.Collections.Generic;
using IbrahKit.Keys;
using IbrahKit.Localization;
using UnityEngine;

#endregion

namespace IbrahKit.Settings
{
    [Serializable]
    public class Setting : IKey
    {
        private float currentValue;
        private DisplayType displayType;

        private Vector2 floatRange;
        private Vector2Int intRange;
        private string key;
        private List<Local_Key> keys;
        private bool loop;

        public Action<Setting> onValueChanged;
        private float step;

        public Setting(string key, Vector2 floatRange, Vector2Int intRange, DisplayType displayType,
            List<Local_Key> keys, bool loop, float step, float defaultValue)
        {
            this.key = key;
            this.floatRange = floatRange;
            this.intRange = intRange;
            this.displayType = displayType;
            this.keys = keys;
            this.loop = loop;
            this.step = step;
            currentValue = defaultValue;
        }

        public void Increment()
        {
            currentValue += step;
            Loop();
        }

        public void Decrement()
        {
            currentValue -= step;
            Loop();
        }

        public void Set(float value)
        {
            currentValue = value;
            Loop();
        }

        private void Loop()
        {
            switch (displayType)
            {
                case DisplayType.NUMBER:
                    if (currentValue > floatRange.y)
                    {
                        currentValue = floatRange.x;
                    }
                    else if (currentValue < floatRange.x)
                    {
                        currentValue = floatRange.y;
                    }

                    break;
                case DisplayType.STRING:
                    if (currentValue > intRange.y)
                    {
                        currentValue = intRange.x;
                    }
                    else if (currentValue < intRange.x)
                    {
                        currentValue = intRange.y;
                    }

                    break;
                case DisplayType.BOOL:
                    if (currentValue > 1)
                    {
                        currentValue = 0;
                    }
                    else if (currentValue < 0)
                    {
                        currentValue = 1;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetValue()
        {
            return currentValue.ToString();
        }
        
        public float GetCurrent() => currentValue;
        public string GetKey()
        {
            return key;
        }
    }
}