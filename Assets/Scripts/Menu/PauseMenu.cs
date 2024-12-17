using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SavesDataSlot;

namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {
        private const string SceneMenu = "Menu";

        [SerializeField] private OptionsMenu _optionsMenu;
        [SerializeField] private SlotDataStorage _slotDataManager;
        [SerializeField] private Button _backToMainMenu;
        [SerializeField] private Button _continue;
        [SerializeField] private Button _settings;

        private float _minWaitTime = 0f;
        private float _maxWaitTime = 1f;

        public bool IsPaused { get; private set; } = false;

        private void Start()
        {
            _backToMainMenu.onClick.AddListener(Load);
            _continue.onClick.AddListener(Resume);
            _settings.onClick.AddListener(OpenSettings);
        }

        public void TogglePause()
        {
            if (!IsPaused)
            {
                Stop();
            }
            else
            {
                Resume();
            }
        }

        private void Resume()
        {
            IsPaused = false;
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        private void Stop()
        {
            IsPaused = true;
            gameObject.SetActive(true);
            Time.timeScale = _minWaitTime;
        }

        private void Load()
        {
            _slotDataManager.Save();
            Time.timeScale = _maxWaitTime;
            SceneManager.LoadScene(SceneMenu);
        }

        private void OpenSettings()
        {
            _optionsMenu.OpenWindow();
            _optionsMenu.SetBackButtonAction(Return);
            gameObject.SetActive(false);
        }

        private void Return()
        {
            _optionsMenu.CloseWindow();
            gameObject.SetActive(true);
        }
    }
}