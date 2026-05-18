using IbrahKit.Settings;
using UnityEngine;

namespace IbrahKit.Settings
{
    public abstract class Setting_Config_Base : ScriptableObject
    {
        [SerializeField] private string key;

        public string GetKey() => key;

        public abstract string GetDefaultValue();

        public abstract bool TryGetInstance(out Setting result);

        public abstract Setting GetDummy();
    }

}