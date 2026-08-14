#region

using System;
using System.Collections.Generic;
using IbrahKit.Debugging;
using IbrahKit.UI.Core.Config;
using UnityEngine;

#endregion

namespace IbrahKit.UI
{
    [CreateAssetMenu(fileName = "NewUIFitterConfig", menuName = "IbrahKit/UI/Modifier/FitterConfig")]
    public class UI_Rect_Fitter_Config : UI_Config<UI_Rect_Fitter_Config>
    {
        [SerializeField] private float margin = 0;

        [SerializeField] private List<PlatformBasedMargin> marginOverride = new();

        public UI_Rect_Fitter_Config(float margin)
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

        [Serializable]
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