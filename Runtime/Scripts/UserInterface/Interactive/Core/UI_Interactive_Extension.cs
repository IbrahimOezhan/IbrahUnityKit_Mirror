using Sirenix.OdinInspector;
using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public abstract class UI_Interactive_Extension : Extension
    {
        [SerializeField, ReadOnly] protected UI_Interactive interactive;

        protected UI_Interactive_Extension(UI_Interactive extension) : base(extension)
        {
            if(extension == null)
            {
                Debug.Log("Extension handler attempted to set to null");
                return;
            }

            interactive = extension;
        }

        public virtual void Validate(SelfValidationResult validationResult, GameObject content)
        {

        }
    }
}