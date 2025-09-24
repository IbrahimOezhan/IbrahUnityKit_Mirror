using UnityEngine;

namespace IbrahKit
{
    [CreateAssetMenu(fileName = "NewUILayoutConfig", menuName = "IbrahKit/UILayoutConfig")]
    public class UI_Layout_Config_SO : ScriptableObject
    {
        [SerializeField] private UI_Layout_Config config;

        public UI_Layout_Config GetConfig() => config;
    }
}