#region

using System.Collections.Generic;
using IbrahKit.Debugging;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    public class UI_Platform_Hide : MonoBehaviour
    {
        [SerializeField] private List<RuntimePlatform> hide = new();

        private void Awake()
        {
            if (hide == null)
            {
                IbrahDebug.LogWarning($"{nameof(hide)} is null");
                return;
            }

            if (hide.Contains(Application.platform))
            {
                gameObject.SetActive(false);
            }
        }

        public virtual bool HideCustom()
        {
            return false;
        }
    }
}