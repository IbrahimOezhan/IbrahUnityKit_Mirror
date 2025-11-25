using IbrahKit.Debug;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Application = UnityEngine.Application;

namespace IbrahKit
{
    [System.Serializable]
    public class UI_Interative_Extension_Fitter : UI_Interactive_Extension
    {
        private Nullable<Vector2> lastPrefferedSize = null;

        private UI_Text_Wrapper text;

        protected RectTransform rect;

        [SerializeField] private GameObject target;

        [SerializeField, HorizontalGroup("Width")]
        protected bool scaleWidth = true;

        [SerializeField, HorizontalGroup("Width"), ShowIf(nameof(scaleWidth))]
        protected int maxWidth;

        [SerializeField, HorizontalGroup("Height")]
        protected bool scaleHeight = true;

        [SerializeField, HorizontalGroup("Height"), ShowIf(nameof(scaleHeight))]
        protected int maxHeight;

        [SerializeField, HorizontalGroup("Height"), ShowIf(nameof(scaleHeight))]
        protected int heightOffset;

        public UI_Interative_Extension_Fitter(UI_Interactive extension) : base(extension)
        {

        }

        protected override bool InitPro()
        {
            text = new(target != null ? target : extension.gameObject);

            bool result = (rect != null || extension.TryGetComponent(out rect)) && text != null && text.GetMode() != UI_Text_Wrapper.Mode.NONE;

            if (!result)
            {
                IbrahDebug.LogWarning($"TryInit failed: rect={rect} text={text} mode={text?.GetMode()}");
                return false;
            }

            interactive.GetMenu().OnFocusOrResolutionChanged += extension.RunExtensions;

            return true;
        }

        protected override void RunPro()
        {
            if (!Init())
            {
                return;
            }

            UI_Fitter_Config config = GetConfig();

            Vector2 preferred = text.GetPreferredSize();

            if (preferred == lastPrefferedSize)
            {
                return;
            }

            lastPrefferedSize = preferred;

            if (scaleWidth) SetSize(text.GetPreferredSize().x, maxWidth, 0, config, RectTransform.Axis.Horizontal);

            if (scaleHeight) SetSize(text.GetPreferredSize().y, maxHeight, heightOffset, config, RectTransform.Axis.Vertical);
        }

        protected override void CleanupPro()
        {
            interactive.GetMenu().OnFocusOrResolutionChanged -= extension.RunExtensions;
        }

        protected override int GetOrderPro()
        {
            return 100;
        }

        private void SetSize(float size, float max, float offset, UI_Fitter_Config config, RectTransform.Axis axis)
        {
            float _max = Mathf.Clamp(size, 0, GetMax(maxHeight));

            GetRect().SetSizeWithCurrentAnchors(axis, _max + config.GetMargin() + offset);
        }

        private UI_Fitter_Config GetConfig()
        {
            UI_Configs.TryGet<UI_Fitter_Config_Override, UI_Fitter_Config_SO, UI_Fitter_Config>(UI_Configs.GetConfigs(extension.transform), out UI_Fitter_Config resolvedConfig);

            resolvedConfig ??= new UI_Fitter_Config(0);

            return resolvedConfig;
        }

        private RectTransform GetRect() => rect != null || Application.isPlaying ? rect : extension.GetComponent<RectTransform>();

        private float GetMax(float max) => max == 0 ? Mathf.Infinity : max;
    }
}