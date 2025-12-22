using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit.UI
{
    public class UI_Settings_Menu : UI_Menu
    {
        public static UI_Settings_Menu Instance;

        protected override void Awake()
        {
            base.Awake();

            Instance = this;
        }
    }
}