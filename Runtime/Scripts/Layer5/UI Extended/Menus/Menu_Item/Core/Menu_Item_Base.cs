#region

using System;
using System.Collections.Generic;
using System.Linq;
using IbrahKit.Debugging;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable]
    public abstract class Menu_Item_Base
    {
        [SerializeField] private SKIP_REASON skipReason;

        [ShowIf(nameof(skipReason), SKIP_REASON.ONLAYOUT), SerializeField]
        private List<string> showOnLayouts;

        private bool Skip(RectTransform parent)
        {
            return skipReason switch
            {
                SKIP_REASON.ONLAYOUT =>
                    UI_Layout_Config.TryGet(parent, out UI_Layout_Config result) && !ShowLayout(result, showOnLayouts),
                SKIP_REASON.ALWAYS => true,
                _ => false
            };
        }

        public static bool ShowLayout(UI_Layout_Config layoutConfig, List<string> layouts)
        {
            return layoutConfig.GetActiveLayouts().Intersect(layouts).Any();
        }

        public bool TrySpawn(RectTransform parent, UI_Menu menu, out GameObject go)
        {
            go = null;

            if (Skip(parent))
            {
                IbrahDebug.Log("Skipped");
                return false;
            }

            if (Spawn(parent, menu, out go)) return true;

            return false;
        }

        protected abstract bool Spawn(RectTransform parent, UI_Menu menu, out GameObject go);

        private enum SKIP_REASON
        {
            DONT,
            ALWAYS,
            ONLAYOUT,
        }
    }
}