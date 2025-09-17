using Sirenix.OdinInspector;
using UnityEngine;
using Application = UnityEngine.Application;

namespace IbrahKit
{
    public abstract class UI_Fitter : UI_Extension
    {
        protected RectTransform rect;

        [SerializeField] protected UI_Fitter_Config_SO customConfig;

        [SerializeField, HorizontalGroup("Width")] protected bool scaleWidth = true;

        [SerializeField, HorizontalGroup("Width"), ShowIf(nameof(scaleWidth))] protected int maxWidth;

        [SerializeField, HorizontalGroup("Height")] protected bool scaleHeight = true;

        [SerializeField, HorizontalGroup("Height"), ShowIf(nameof(scaleHeight))] protected int maxHeight;

        [SerializeField, HorizontalGroup("Height"), ShowIf(nameof(scaleHeight))] protected int heightOffset;

        protected override void Init()
        {
            if (rect == null && !TryGetComponent(out rect))
            {
                return;
            }

            base.Init();
        }

        protected void SetSize(float size, float max, float offset, UI_Fitter_Config config, RectTransform.Axis axis)
        {
            float _max = Mathf.Clamp(size, 0, GetMax(maxHeight));
            rect.SetSizeWithCurrentAnchors(axis, _max + config.GetMargin() + offset);
        }

        protected float GetMax(float max)
        {
            return max == 0 ? Mathf.Infinity : max;
        }

        protected UI_Fitter_Config GetConfig()
        {
            if (customConfig != null)
            {
                return customConfig.GetConfig();
            }

            UI_Fitter_Config resolvedConfig = customConfig ? customConfig.GetConfig() : null;

            if (UI_Configs.GetFitter(UI_Configs.GetConfigs(transform), out UI_Fitter_Config_SO result))
            {
                resolvedConfig = result.GetConfig();
            }

            resolvedConfig = resolvedConfig ?? new UI_Fitter_Config(0);

            return resolvedConfig;
        }

        protected RectTransform GetRect()
        {
            if (!Application.isPlaying)
            {
                return rect != null ? rect : GetComponent<RectTransform>();
            }
            else
            {
                return rect;
            }
        }

        public override int GetOrder()
        {
            return 100;
        }
    }
}