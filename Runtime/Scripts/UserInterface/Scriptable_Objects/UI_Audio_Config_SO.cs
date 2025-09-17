using UnityEngine;

namespace IbrahKit
{
    [CreateAssetMenu(fileName = "NewAudioConfig", menuName = "IbrahKit/AudioConfig")]
    public class UI_Audio_Config_SO : ScriptableObject
    {
        [SerializeReference] private UI_Audio_Config config;

        public UI_Audio_Config GetConfig() => config;
    }
}