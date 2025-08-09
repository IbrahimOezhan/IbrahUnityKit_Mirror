using Sirenix.OdinInspector;
using System;
using System.Text.Json;
using UnityEngine;
using UnityEngine.Events;

namespace IbrahKit
{
    /// <summary>
    /// Provides the base functionality for all elements that the game can have
    /// </summary>
    [Serializable]
    public class Setting
    {
        private bool init;

        [BoxGroup("Base"), SerializeField, Dropdown(Local_Manager.DROP)] private string settingsKey;

        [BoxGroup("Value"), SerializeField] private float defaultValue;

        [BoxGroup("Value"), SerializeField] private float value;

        [BoxGroup("Value"), SerializeField] private bool loop;

        [BoxGroup("ValueRange"), SerializeField] private Vector2 valueRange;

        [BoxGroup("Display"), SerializeField] private DisplayMode displayMode;

        [BoxGroup("Display"), Dropdown(Local_Manager.DROP), SerializeField, ShowIf(nameof(displayMode), DisplayMode.KEY)] private string[] keys;

        [BoxGroup("Other Properties"), SerializeField] private SettingsType type;

        [BoxGroup("Other Properties"), SerializeField, ShowIf(nameof(type), SettingsType.RANGE)] private float steps;

        [BoxGroup("Other Properties"), SerializeField] private UnityEvent OnValueChange;

        public virtual void Init(string initialValue)
        {
            if (init) return;

            if (!float.TryParse(initialValue, out value))
            {
                SetValue(GetDefault());
            }
            else
            {
                ApplyChanges();
            }

            init = true;
        }

        public virtual void AddValue(float value)
        {
            SetValue(GetValue() + value);
        }

        public virtual void SetValue(float value)
        {
            if (loop)
            {
                this.value = Number_Utilities.LoopNumber(value, GetValueRange().x, GetValueRange().y);
            }
            else
            {
                this.value = Mathf.Clamp(value, GetValueRange().x, GetValueRange().y);
            }

            ApplyChanges();
        }

        public virtual void ApplyChanges()
        {
            OnValueChange.Invoke();
        }

        public virtual void SetValueRange(Vector2 newRange)
        {
            valueRange = newRange;
        }

        public UnityEvent GetEvent()
        {
            return OnValueChange;
        }

        public Setting_Local_Json GetLocal()
        {
            JsonSerializerOptions options = new()
            {
                IncludeFields = true
            };

            string json = Local_Manager.Instance.GetString(settingsKey);

            try
            {
                Setting_Local_Json local = JsonSerializer.Deserialize<Setting_Local_Json>(json, options);

                return local;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                Debug.Log(json);

                return new();
            }
        }

        public Vector2 GetValueRange()
        {
            return valueRange;
        }

        public SettingsType GetSettingsType()
        {
            return type;
        }

        public virtual string GetDisplayValue()
        {
            switch (displayMode)
            {
                case DisplayMode.RAW:
                    return value.ToString("0.0");
                case DisplayMode.INT:
                    return value.ToString("0");
                case DisplayMode.KEY:
                    return Local_Manager.Instance.GetString(keys[(int)(value / steps)]);
            }

            return "ERROR";
        }

        public string GetKey()
        {
            return settingsKey;
        }

        public float GetStep()
        {
            return steps;
        }

        public virtual float GetDefault()
        {
            return defaultValue;
        }

        public float GetValue()
        {
            return value;
        }

        public string[] GetKeys()
        {
            return keys;
        }

        public bool GetLoop()
        {
            return loop;
        }

        private enum DisplayMode
        {
            RAW,
            INT,
            KEY,
        }
    }
}