using UnityEngine;

namespace IbrahKit
{
    public class UI_Audio_Manager : MonoBehaviour
    {
        [SerializeField] private UI_Audio_SO defaultAudio;

        public static UI_Audio_Manager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void OnClick(UI_Audio_SO overrideAudio)
        {
            if(overrideAudio != null)
            {
                overrideAudio.OnClick();
                return;
            }

            if(defaultAudio != null)
            {
                defaultAudio.OnClick();
                return;
            }

            Debug.LogWarning("No audio source defined");
        }

        public void OnHover(UI_Audio_SO overrideAudio)
        {
            if (overrideAudio != null)
            {
                overrideAudio.OnHover();
                return;
            }

            if (defaultAudio != null)
            {
                defaultAudio.OnHover();
                return;
            }

            Debug.LogWarning("No audio source defined");
        }
    }
}