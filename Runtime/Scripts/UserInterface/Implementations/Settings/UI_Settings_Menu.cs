using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit.UI
{
    public class UI_Settings_Menu : MonoBehaviour
    {
        [SerializeField, Required] private UI_Menu menu;

        public static UI_Menu Instance;

        private void Awake()
        {
            Instance = menu;
        }
    }
}