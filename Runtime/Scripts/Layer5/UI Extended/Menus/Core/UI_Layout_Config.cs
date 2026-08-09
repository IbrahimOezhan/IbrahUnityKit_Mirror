#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace IbrahKit.UI.Menu
{
    [CreateAssetMenu(fileName = "NewUILayoutConfig", menuName = "IbrahKit/UI_Layout_Config")]
    public class UI_Layout_Config : Config<UI_Layout_Config>
    {
        [SerializeField] private List<string> activeLayouts = new();

        public List<string> GetActiveLayouts()
        {
            return activeLayouts;
        }
    }
}