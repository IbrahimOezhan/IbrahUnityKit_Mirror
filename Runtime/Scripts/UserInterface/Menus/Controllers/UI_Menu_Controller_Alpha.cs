using UnityEngine;

namespace IbrahKit
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_Menu_Controller_Alpha : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        [SerializeField, HideInInspector] private int hiddenCount = 0;
        [SerializeField, HideInInspector] private float enabledAlpha;

        public void SetEnabledAlpha(float value)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

            enabledAlpha = value;

            Render();
        }

        public void PassHiddenCount(int count)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

            hiddenCount = count;

            Render();
        }

        private void Render()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

            canvasGroup.alpha = enabledAlpha * (hiddenCount == 0 ? 1 : 0);
        }

        public void SetInteractable(bool value)
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

            canvasGroup.blocksRaycasts = value;
        }
    }
}
