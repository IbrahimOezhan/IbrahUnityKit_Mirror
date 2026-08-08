#region

using IbrahKit.Manager;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class UI_Config_Manager : Manager_Global<UI_Config_Manager, UI_Config_Manager_Data>
    {
        public UI_Configs GetConfigs() => GetManagerData().GetConfigs();

        public T GetConfig<T>(params Override_Config_SO<T>[] configs) where T : ScriptableObject
        {
            foreach (var t in configs)
            {
                if (t.TryGet(out T value))
                {
                    return value;
                }
            }

            return default;
        }
    }
}