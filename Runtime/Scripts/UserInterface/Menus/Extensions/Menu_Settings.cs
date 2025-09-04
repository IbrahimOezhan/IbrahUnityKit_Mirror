using UnityEngine;

namespace IbrahKit
{
    public class Menu_Settings : MonoBehaviour
    {
        [SerializeField] private UI_Menu menu;

        public static UI_Menu Instance;

        private void Awake()
        {
            Instance = menu;
        }
    }
}