using Levels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace Menu.Level
{
    public class LevelEndMenu : MonoBehaviour
    {
        private const string SceneMenu = "Menu";

        [SerializeField] private IntervalWindow _window;
        [SerializeField] private Button _continue;
        [SerializeField] private Button _exitButton;
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private ParticleSystem _particleSystem;

        private void Start()
        {
            _exitButton.onClick.AddListener(OnExitClicked);
            _continue.onClick.AddListener(OnContinueClicked);
        }

        public void Show()
        {
            _window.gameObject.SetActive(true);
            Instantiate(_particleSystem, _window.transform);
        }

        private void OnContinueClicked()
        {
            YandexGame.OpenFullAdEvent += OnAdOpened;
            YandexGame.CloseFullAdEvent += OnAdClosed;

            YandexGame.FullscreenShow();
            _window.gameObject.SetActive(false);
            _levelLoader.Continue();
        }

        private void OnExitClicked()
        {
            _window.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneMenu);
        }

        private void OnAdOpened()
        {
            Time.timeScale = 0f;
        }

        private void OnAdClosed()
        {
            Time.timeScale = 1f;

            YandexGame.OpenFullAdEvent -= OnAdOpened;
            YandexGame.CloseFullAdEvent -= OnAdClosed;
        }
    }
}