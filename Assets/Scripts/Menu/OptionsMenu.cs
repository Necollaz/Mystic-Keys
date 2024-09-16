using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _generalSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Toggle _mute;
    [SerializeField] private Graphic _targetGraphic;
    [SerializeField] private Graphic _muteGraphic;
    [SerializeField] private Button _backButton;

    private SettingMusic _settingMusic;

    public event Action OnCloseSettings;
    public event Action OnOpenSettings;

    private void Start()
    {
        Initialize();
        _backButton.onClick.AddListener(CloseWindow);
    }

    public void CloseWindow()
    {
        OnCloseSettings?.Invoke();
        gameObject.SetActive(false);
    }

    public void OpenWindow()
    {
        OnOpenSettings?.Invoke();
        gameObject.SetActive(true);
    }

    private void Initialize()
    {
        _settingMusic = new SettingMusic(_mixer, _generalSlider, _musicSlider, _mute, _targetGraphic, _muteGraphic);
        _settingMusic.Initialize();
    }
}