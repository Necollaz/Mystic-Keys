using UnityEngine;
using UnityEngine.Audio;

namespace Music
{
    public class VolumeControl
    {
        private AudioMixer _mixer;

        public VolumeControl(AudioMixer mixer)
        {
            _mixer = mixer;
        }

        public void SetVolume(string parameterName, float volume)
        {
            float clampedVolume = Mathf.Clamp(volume, 0.0001f, 1f);
            float dbVolume = Mathf.Log10(clampedVolume) * 20;

            _mixer.SetFloat(parameterName, dbVolume);
            PlayerPrefs.SetFloat(parameterName, volume);
        }
    }
}