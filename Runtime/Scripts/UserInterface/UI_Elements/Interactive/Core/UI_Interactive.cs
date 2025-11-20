using UnityEngine;

namespace IbrahKit
{
    public class UI_Interactive : UI_Base
    {
        [SerializeField] private Extension_Handler<UI_Interactive_Extension> extensionHandler;

        public override void OnMenuElementChanged()
        {
            extensionHandler.RunExtensions();
        }

        public override void OnMenuEnabled()
        {
            extensionHandler.RunExtensions();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            extensionHandler.Cleanup();
        }
    }
}