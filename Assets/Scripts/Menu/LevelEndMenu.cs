using UnityEngine;
using UnityEngine.UI;

public class LevelEndMenu : MonoBehaviour
{
    [SerializeField] private IntervalMenu _levelCompletedWindow;
    [SerializeField] private Button _continue;
    [SerializeField] private Button _exit;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private ParticleSystem _particleSystem;

    private void Start()
    {
        _continue.onClick.AddListener(OnContinueClicked);
        _exit.onClick.AddListener(OnExitClicked);
    }

    public void ShowLevelCompletedWindow()
    {
        _levelCompletedWindow.gameObject.SetActive(true);
        Instantiate(_particleSystem, _levelCompletedWindow.transform);
    }

    private void OnContinueClicked()
    {
        _levelCompletedWindow.gameObject.SetActive(false);
        _levelLoader.ContinueToNextLevel();
    }

    private void OnExitClicked()
    {
        _levelCompletedWindow.gameObject.SetActive(false);
        _levelLoader.ExitToMainMenu();
    }
}