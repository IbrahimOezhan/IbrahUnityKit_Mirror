#region

using System.Collections.Generic;
using IbrahKit.UI.Core.Config;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [CreateAssetMenu(fileName = "NewUILayoutConfig", menuName = "IbrahKit/UI/Menu/LayoutConfig")]
    public class UI_Layout_Config : UI_Config<UI_Layout_Config>
    {
        [SerializeField] private List<string> activeLayouts = new();

        public List<string> GetActiveLayouts()
        {
            return activeLayouts;
        }
    }
}