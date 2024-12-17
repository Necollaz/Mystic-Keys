using UnityEngine;
using UnityEngine.UI;

namespace Levels
{
    public class RestartGameConfirmation : MonoBehaviour
    {
        [SerializeField] private Button _restartLevel;
        [SerializeField] private Button _restartGame;
        [SerializeField] private LevelLoader _levelLoader;

        private void Start()
        {
            _restartLevel.onClick.AddListener(_levelLoader.RestartCurrent);
            _restartGame.onClick.AddListener(_levelLoader.RestartAll);
        }
    }
}