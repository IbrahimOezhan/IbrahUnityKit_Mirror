using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public abstract class UI_Interactive_Extension : Extension
    {
        protected UI_Interactive interactive;

        protected UI_Interactive_Extension(UI_Interactive extension) : base(extension)
        {
            interactive = extension;
        }

        public virtual void Validate(SelfValidationResult validationResult, GameObject content)
        {

        }
    }
}