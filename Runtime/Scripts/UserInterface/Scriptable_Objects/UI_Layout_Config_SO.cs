using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    [CreateAssetMenu(fileName = "NewUILayoutConfig", menuName = "IbrahKit/UILayoutConfig")]
    public class UI_Layout_Config_SO : ScriptableObject
    {
        [SerializeField] private List<string> activeLayouts = new();

        public List<string> GetActiveLayouts()
        {
            return activeLayouts;
        }
    }
}