#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

namespace IbrahKit.UI.Modifier
{
    [Serializable]
    public abstract class UI_Modifier_Extension : Extension.Extension
    {
        [FormerlySerializedAs("interactive")] [SerializeField, ReadOnly]
        protected UI_Modifier modifier;

        protected UI_Modifier_Extension(UI_Modifier extension) : base(extension)
        {
            if (extension == null)
            {
                Debug.Log("Extension handler attempted to set to null");
                return;
            }

            modifier = extension;
        }

        public virtual void Validate(SelfValidationResult validationResult, GameObject content)
        {
        }
    }
}