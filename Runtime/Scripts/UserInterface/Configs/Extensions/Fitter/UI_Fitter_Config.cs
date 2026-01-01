using IbrahKit.Debugging;
using System.Collections.Generic;
using UnityEngine;

namespace IbrahKit.UI
{
    [System.Serializable]
    public class UI_Fitter_Config : UI_Config
    {
        [SerializeField] private float margin;

        [SerializeField] private List<PlatformBasedMargin> marginOverride = new();

        public UI_Fitter_Config(float margin)
        {
            this.margin = margin;
        }

        public float GetMargin()
        {
            if (marginOverride == null)
            {
                IbrahDebug.LogWarning($"{nameof(marginOverride)} is null");
                return margin;
            }

            for (int i = 0; i < marginOverride.Count; i++)
            {
                if (marginOverride[i].IsPlatform()) return marginOverride[i].GetMargin();
            }

            return margin;
        }

        [System.Serializable]
        private class PlatformBasedMargin
        {
            [SerializeField] private float margin;

            [SerializeField] private RuntimePlatform platform;

            public float GetMargin() => margin;

            public bool IsPlatform()
            {
                return Application.platform == platform;
            }
        }
    }
}