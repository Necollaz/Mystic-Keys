using UnityEngine;
using UnityEngine.UI;

namespace Levels.LevelMessage
{
    public class LevelMessage : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void Awake()
        {
            _closeButton.onClick.AddListener(CloseMessage);
        }

        private void CloseMessage()
        {
            gameObject.SetActive(false);
        }
    }
}