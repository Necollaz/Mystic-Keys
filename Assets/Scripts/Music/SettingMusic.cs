using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMusic
{
    private AudioMixer _mixer;
    private Slider _generalSlider;
    private Slider _musicSlider;
    private Toggle _mute;
    private Graphic _targetGraphic;
    private Graphic _muteGraphic;
    private VolumeControl _volumeControl;
    private MuteControl _muteControl;
    private UIUpdates _uiUpdates;

    public SettingMusic(AudioMixer mixer, Slider generalSlider, Slider musicSlider, Toggle mute, Graphic targetGraphic, Graphic muteGraphic)
    {
        _mixer = mixer;
        _generalSlider = generalSlider;
        _musicSlider = musicSlider;
        _mute = mute;
        _targetGraphic = targetGraphic;
        _muteGraphic = muteGraphic;
    }

    public void Initialize()
    {
        _volumeControl = new VolumeControl(_mixer);
        _muteControl = new MuteControl();
        _uiUpdates = new UIUpdates(_targetGraphic, _muteGraphic);

        SetVolume("AllMusicVolume", _generalSlider);
        SetVolume("MusicVolume", _musicSlider);

        _generalSlider.onValueChanged.AddListener(volume => ChangedVolume(volume));
        _musicSlider.onValueChanged.AddListener(volume => _volumeControl.SetVolume("MusicVolume", volume));
        _mute.onValueChanged.AddListener(isOn => MuteToggled(isOn));

        _uiUpdates.UpdateGraphic(_mute.isOn, _mute);
    }

    private void SetVolume(string parameterName, Slider slider)
    {
        float defaultVolume = 0.75f;
        slider.value = PlayerPrefs.GetFloat(parameterName, defaultVolume);
        _volumeControl.SetVolume(parameterName, slider.value);
    }

    private void ChangedVolume(float volume)
    {
        _volumeControl.SetVolume("AllMusicVolume", volume);
        _volumeControl.SetVolume("EffectsVolume", volume);
    }

    private void MuteToggled(bool isOn)
    {
        _muteControl.Toggle(isOn, _volumeControl, _generalSlider.value);
        _uiUpdates.UpdateGraphic(isOn, _mute);
    }
}