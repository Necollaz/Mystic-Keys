using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Music
{
    public class SettingMusic
    {
        private const string AllVolumeParam = "AllMusicVolume";
        private const string MusicVolumeParam = "MusicVolume";
        private const string EffectsVolumeParam = "EffectsVolume";

        private AudioMixer _mixer;
        private Slider _generalSlider;
        private Slider _musicSlider;
        private Slider _effectSlider;

        private VolumeControl _volumeControl;

        public SettingMusic(AudioMixer mixer, Slider generalSlider, Slider musicSlider, Slider effectSlider)
        {
            _mixer = mixer;
            _generalSlider = generalSlider;
            _musicSlider = musicSlider;
            _effectSlider = effectSlider;
        }

        public void Initialize()
        {
            _volumeControl = new VolumeControl(_mixer);

            SetVolume(AllVolumeParam, _generalSlider, defaultVolume: 0.75f);
            SetVolume(MusicVolumeParam, _musicSlider, defaultVolume: 0.75f);
            SetVolume(EffectsVolumeParam, _effectSlider, defaultVolume: 0.75f);

            _generalSlider.onValueChanged.AddListener(volume => _volumeControl.SetVolume(AllVolumeParam, volume));
            _musicSlider.onValueChanged.AddListener(volume => _volumeControl.SetVolume(MusicVolumeParam, volume));
            _effectSlider.onValueChanged.AddListener(volume => _volumeControl.SetVolume(EffectsVolumeParam, volume));
        }

        private void SetVolume(string parameterName, Slider slider, float defaultVolume)
        {
            float volume = PlayerPrefs.GetFloat(parameterName, defaultVolume);
            slider.value = volume;

            _volumeControl.SetVolume(parameterName, volume);
        }
    }
}