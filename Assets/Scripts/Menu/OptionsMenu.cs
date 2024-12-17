using Music;
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
        [SerializeField] private Slider _effectSlider;
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

        public void SetBackButtonAction(UnityAction action)
        {
            _backButton.onClick.RemoveAllListeners();
            _backButton.onClick.AddListener(action);
        }

        private void Initialize()
        {
            _settingMusic = new SettingMusic(_mixer, _generalSlider, _musicSlider, _effectSlider);

            _settingMusic.Initialize();
        }
    }
}