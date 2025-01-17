using UnityEngine;
using UnityEngine.Audio;

namespace Music
{
    public class AudioPauseOnFocus : MonoBehaviour
    {
        private const string AllVolume = "AllMusicVolume";
        
        [SerializeField] private AudioMixer _audioMixer;
        
        private readonly float _mutedVolume = -80f;
        private float _originalVolume = 0f;
        
        private void Start()
        {
            if (!_audioMixer.GetFloat(AllVolume, out _originalVolume))
            {
                _originalVolume = 0f;
            }
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                _audioMixer.SetFloat(AllVolume, _originalVolume);
            }
            else
            {
                _audioMixer.SetFloat(AllVolume, _mutedVolume);
            }
        }
    }
}