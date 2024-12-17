using UnityEngine;

namespace Menu
{
    public class UICanvasGroupButtons : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _gameUI;
        [SerializeField] private PauseMenu _pauseMenu;

        private void Update()
        {
            if (_pauseMenu.IsPaused)
            {
                _gameUI.interactable = false;
                _gameUI.blocksRaycasts = false;
            }
            else
            {
                _gameUI.interactable = true;
                _gameUI.blocksRaycasts = true;
            }
        }
    }
}