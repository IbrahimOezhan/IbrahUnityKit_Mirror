using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit
{
    public class UI_PlatformHide : MonoBehaviour
    {
        [SerializeField]
        private List<RuntimePlatform> hide = new();

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