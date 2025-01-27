using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private OptionsMenu _optionsMenu;
        [SerializeField] private Button _menuButton;
        
        private readonly KeyCode _pauseKey = KeyCode.Escape;
        
        private void Start()
        {
            _menuButton.onClick.AddListener(TogglePauseMenu);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(_pauseKey))
            {
                TogglePauseMenu();
            }
        }
        
        private void TogglePauseMenu()
        {
            if (_pauseMenu.gameObject.activeSelf)
            {
                HidePauseMenu();
            }
            else
            {
                ShowPauseMenu();
            }
        }
        
        private void ShowPauseMenu()
        {
            _pauseMenu.TogglePause();
            _optionsMenu.CloseWindow();
        }
        
        private void HidePauseMenu()
        {
            _pauseMenu.TogglePause();
        }
    }
}