using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Menu
{
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

        private void Start()
        {
            Initialize();
        }

        public void CloseWindow()
        {
            gameObject.SetActive(false);
        }

        public void OpenWindow()
        {
            gameObject.SetActive(true);
        }

        private void Initialize()
        {
            _settingMusic = new SettingMusic(_mixer, _generalSlider, _musicSlider, _mute, _targetGraphic, _muteGraphic);
            _settingMusic.Initialize();
        }

        public void SetBackButtonAction(UnityAction action)
        {
            _backButton.onClick.RemoveAllListeners();
            _backButton.onClick.AddListener(action);
        }
    }
}