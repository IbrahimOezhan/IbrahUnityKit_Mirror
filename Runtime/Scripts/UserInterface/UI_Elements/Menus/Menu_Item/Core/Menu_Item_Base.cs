using IbrahKit.Debug;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit.UI
{
    public abstract class Menu_Item_Base
    {
        [SerializeField] private SKIP_REASON skipReason;

        [ShowIf("skipreason", SKIP_REASON.ONLAYOUT), SerializeField] private List<string> showOnLayouts;

        private bool Skip(RectTransform parent)
        {
            switch (skipReason)
            {
                case SKIP_REASON.ONLAYOUT:
                    return UI_Configs.TryGet<UI_Layout_Config_Override, UI_Layout_Config_SO, UI_Layout_Config>(UI_Configs.GetConfigs(parent), out UI_Layout_Config result)
            && !UI_Config_Manager.GetInstance().ShowLayout(result, showOnLayouts);
                case SKIP_REASON.ALWAYS:
                    return true;
            }

            return false;
        }

        private enum SKIP_REASON
        {
            DONT,
            ALWAYS,
            ONLAYOUT,
        }

        public bool TrySpawn(RectTransform parent, UI_Menu menu, out GameObject go)
        {
            go = null;

            if (Skip(parent))
            {
                IbrahDebug.Log("Skipped");
                return false;
            }

            if (TrySpawnPro(parent, menu, out go)) return true;

            return false;
        }

        protected abstract bool TrySpawnPro(RectTransform parent, UI_Menu menu, out GameObject go);
    }
}