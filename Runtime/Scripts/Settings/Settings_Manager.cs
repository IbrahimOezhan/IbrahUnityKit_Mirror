using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using UnityEditor.Overlays;
using UnityEngine;

namespace IbrahKit
{
    [DefaultExecutionOrder(Execution_Order.settings)]
    public class Settings_Manager : Manager_DDOL<Settings_Manager>
    {
        public const string SAVE_DATA_KEY = "Settings";
        private const string NONE = "None";

        private SaveData saveData;

        [SerializeField, OnValueChanged(nameof(OnValueChanged)), ValueDropdown(nameof(GetAllTypesDropdownFormat))]
        private string addSetting = NONE;

        [SerializeReference] private List<Setting> settings = new();

        private void Start()
        {
            if (GetInstance() == this)
            {
                saveData = (SaveData)Save_Manager.GetInstance().Load(SAVE_DATA_KEY, new SaveData());

                for (int i = 0; i < settings.Count; i++)
                {
                    string key = settings[i].GetKey();
                    settings[i].Init(saveData.GetValue(key));
                }
            }
        }

        private void OnDestroy()
        {
            if (GetInstance() == this)
            {
                for (int i = 0; i < settings.Count; i++)
                {
                    string key = settings[i].GetKey();

                    saveData.SetValue(key, settings[i].GetValue().ToString());
                }

                if (GetInstance() == this)
                {
                    if (Save_Manager.TryGet(out Save_Manager result))
                    {
                        result.Return(SAVE_DATA_KEY, saveData);
                    }
                }
            }
        }

        [Button(Name = "Validate")]
        private void OnValidate()
        {
            Dropdown_Utilities.CreateDropdown(settings.Select(x => x.GetKey()).ToList(), SAVE_DATA_KEY);
        }

        private IEnumerable GetAllTypesDropdownFormat() { return Type_Utilities.GetAllTypesDropdownFormat(typeof(Setting)); }

        private void OnValueChanged()
        {
            if (addSetting == NONE) return;

            List<Type> types = Type_Utilities.GetAllTypes(typeof(Setting)).ToList();

            Type type = types.Find(x => x.Name == addSetting);

            if (type != null)
            {
                settings.Add((Setting)Activator.CreateInstance(type));
            }

            addSetting = NONE;
        }

        public void OpenSettings(UI_Menu _origin)
        {
            if (_origin == null)
            {
                Debug.LogWarning("Provided origin menu is null");
                return;
            }

            _origin.GetStateController().Transition<Menu_Transition_Instant>(Menu_Settings.Instance);
        }

        public bool GetSetting(string _key, out Setting setting)
        {
            setting = null;

            if (String_Utilities.IsEmpty(_key))
            {
                Debug.LogWarning("Provided key is empty or null");
                return false;
            }

            setting = settings.Find(x => x.GetKey().Equals(_key));

            if (setting == null)
            {
                Debug.LogWarning("No setting found with key: " + _key);
                return false;
            }

            return true;
        }

        [Serializable]
        private class SaveData : Savable
        {
            [JsonInclude]
            private Dictionary<string, string> data = new();

            public string GetValue(string key)
            {
                if (data.TryGetValue(key, out string value))
                {
                    return value;
                }

                return "";
            }

            public void SetValue(string key, string value)
            {
                if (data.ContainsKey(key))
                {
                    data[key] = value;
                }
                else data.TryAdd(key, value);
            }
        }
    }
}