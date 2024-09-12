using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMusic : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _generalSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Toggle _mute;
    [SerializeField] private Graphic _targetGraphic;
    [SerializeField] private Graphic _muteGraphic;

    private VolumeControl _volumeControl;
    private MuteControl _muteControl;
    private UIUpdates _uiUpdates;

    private void Start()
    {
        InitializeSettings();
    }

    private void InitializeSettings()
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