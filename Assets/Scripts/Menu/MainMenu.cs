using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        private const string SceneGame = "Game";

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private MainMenuController _uiManager;

        private void Start()
        {
            _startButton.onClick.AddListener(StartGame);
            _settingsButton.onClick.AddListener(OpenSettings);
            _uiManager.ShowMainMenu();
        }

        private void StartGame()
        {
            SceneManager.LoadScene(SceneGame);
        }

        private void OpenSettings()
        {
            _uiManager.ShowOptionsMenu();
        }
    }
}