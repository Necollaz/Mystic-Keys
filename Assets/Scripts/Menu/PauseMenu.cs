using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private OptionsMenu _optionsMenu;
    [SerializeField] private Button _backToMainMenu;
    [SerializeField] private Button _continue;
    [SerializeField] private Button _settings;

    private float _minWaitTime = 0f;
    private float _maxWaitTime = 1f;
    private bool _isPause = false;

    private void Start()
    {
        _backToMainMenu.onClick.AddListener(LoadMenu);
        _continue.onClick.AddListener(Resume);
        _settings.onClick.AddListener(OpenSettings);
    }

    public void TogglePause()
    {
        if (!_isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    private void Resume()
    {
        _isPause = false;
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        _isPause = true;
        gameObject.SetActive(true);
        Time.timeScale = _minWaitTime;
    }

    private void LoadMenu()
    {
        Time.timeScale = _maxWaitTime;
        SceneManager.LoadScene("Menu");
    }

    private void OpenSettings()
    {
        _optionsMenu.OpenWindow();
        _optionsMenu.SetBackButtonAction(ReturnToPauseMenu);
        gameObject.SetActive(false);
    }

    public void ReturnToPauseMenu()
    {
        _optionsMenu.CloseWindow();
        gameObject.SetActive(true);
    }
}