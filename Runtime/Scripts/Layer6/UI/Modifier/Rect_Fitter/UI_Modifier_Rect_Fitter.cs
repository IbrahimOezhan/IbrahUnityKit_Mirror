#region

using System;
using IbrahKit.Debugging;
using IbrahKit.UI.Generic;
using IbrahKit.UI.Modifier;
using IbrahKit.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using Application = UnityEngine.Application;

#endregion

namespace IbrahKit.UI
{
    [Serializable]
    public class UI_Modifier_Rect_Fitter : MonoBehaviour
    {
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

        private Nullable<Vector2> lastPreferredSize = null;

        protected RectTransform rect;

        private UI_Text_Wrapper text;

        private void Awake()
        {
            text = new(target != null ? target : gameObject);

            bool result = (rect != null || TryGetComponent(out rect)) && text != null &&
                          text.GetMode() != UI_Text_Wrapper.Mode.NONE;

            if (!result)
            {
                IbrahDebug.LogWarning($"TryInit failed: rect={rect} text={text} mode={text?.GetMode()}");
                return;
            }

            //transform.BetterGetComponentInParent<UI_Canvas_Controller>().OnFocusOrResolutionChanged +=
        }

        protected void Execute()
        {
            UI_Rect_Fitter_Config config = GetConfig();

            Vector2 preferred = text.GetPreferredSize();

            if (preferred == lastPreferredSize)
            {
                return;
            }

            lastPreferredSize = preferred;

            if (scaleWidth) SetSize(text.GetPreferredSize().x, maxWidth, 0, config, RectTransform.Axis.Horizontal);

            if (scaleHeight)
                SetSize(text.GetPreferredSize().y, maxHeight, heightOffset, config, RectTransform.Axis.Vertical);
        }

        private void OnDestroy()
        {
            //transform.BetterGetComponentInParent<UI_Canvas_Controller>().OnFocusOrResolutionChanged -=extension.RunExtensions;
        }

        private void SetSize(float size, float max, float offset, UI_Rect_Fitter_Config config, RectTransform.Axis axis)
        {
            float _max = Mathf.Clamp(size, 0, GetMax(maxHeight));

            GetRect().SetSizeWithCurrentAnchors(axis, _max + config.GetMargin() + offset);
        }

        private UI_Rect_Fitter_Config GetConfig()
        {
            UI_Configs.TryGet<UI_Rect_Fitter_Config_Override, UI_Rect_Fitter_Config_SO, UI_Rect_Fitter_Config>(
                UI_Configs.GetConfigs(transform), out UI_Rect_Fitter_Config resolvedConfig);

            resolvedConfig ??= new UI_Rect_Fitter_Config(0);

            return resolvedConfig;
        }

        private RectTransform GetRect() =>
            rect != null || Application.isPlaying ? rect : GetComponent<RectTransform>();

        private float GetMax(float max) => max == 0 ? Mathf.Infinity : max;
    }
}