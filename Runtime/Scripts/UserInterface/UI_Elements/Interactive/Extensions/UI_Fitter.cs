using Sirenix.OdinInspector;
using UnityEngine;
using Application = UnityEngine.Application;

namespace IbrahKit
{
    public class UI_Fitter : UI_Interactive_Extension
    {
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

        public UI_Fitter(GameObject go) : base(go)
        {
        }

        protected override bool InitPro()
        {
            text = new(target != null ? target : go);

            bool result = (rect != null || go.TryGetComponent(out rect)) && text != null && text.GetMode() != UI_Text_Wrapper.Mode.NONE;

            if (!result)
            {
                IbrahDebug.LogWarning($"TryInit failed: rect={rect} text={text} mode={text?.GetMode()}");
            }

            return result;
        }

        protected override int GetOrderPro()
        {
            return 100;
        }

        protected override void RunPro()
        {
            if (!Init()) return;

            UI_Fitter_Config config = GetConfig();

            if (scaleWidth) SetSize(text.GetPreferredSize().x, maxWidth, 0, config, RectTransform.Axis.Horizontal);

            if (scaleHeight) SetSize(text.GetPreferredSize().y, maxHeight, heightOffset, config, RectTransform.Axis.Vertical);
        }

        protected override void CleanupPro()
        {

        }

        private void SetSize(float size, float max, float offset, UI_Fitter_Config config, RectTransform.Axis axis)
        {
            float _max = Mathf.Clamp(size, 0, GetMax(maxHeight));

            GetRect().SetSizeWithCurrentAnchors(axis, _max + config.GetMargin() + offset);
        }

        private UI_Fitter_Config GetConfig()
        {
            UI_Configs.TryGet<UI_Fitter_Config_Override, UI_Fitter_Config_SO, UI_Fitter_Config>(UI_Configs.GetConfigs(go.transform), out UI_Fitter_Config resolvedConfig);

            resolvedConfig ??= new UI_Fitter_Config(0);

            return resolvedConfig;
        }

        private RectTransform GetRect() => rect != null || Application.isPlaying ? rect : go.GetComponent<RectTransform>();

        private float GetMax(float max) => max == 0 ? Mathf.Infinity : max;
    }
}