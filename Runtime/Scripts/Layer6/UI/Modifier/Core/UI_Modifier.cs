#region

using System;
using IbrahKit.Extension;
using IbrahKit.UI.Generic;
using Sirenix.OdinInspector;

#endregion

namespace IbrahKit.UI.Modifier
{
    public class UI_Modifier : Extension_Handler<UI_Modifier_Extension>, IUIInit, ISelfValidator
    {
        protected void OnDisable()
        {
            Cleanup();
        }

        public void Validate(SelfValidationResult result)
        {
            foreach (UI_Modifier_Extension extension in GetExtensions())
            {
                extension.Validate(result, gameObject);
            }
        }

        public void OnMenuInitBottomUp()
        {
            InitExtensions();

            RunExtensions();
        }

        public void OnMenuInitTopDown()
        {
            throw new NotImplementedException();
        }
    }
}