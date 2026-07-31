#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [Serializable]
    public class UI_Layout_Config : UI_Config
    {
        [SerializeField] private List<string> activeLayouts = new();

        public List<string> GetActiveLayouts()
        {
            return activeLayouts;
        }
    }
}