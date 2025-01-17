using UnityEngine;
using UnityEngine.UI;

namespace Levels.UnlockSlots
{
    public class UnlockSlotsWindow : MonoBehaviour
    {
        [SerializeField] private StorePurchaseSlots _window;
        [SerializeField] private Button _openWindowButton;
        [SerializeField] private Button _closeWindowButton;
        
        private void Start()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            _window.gameObject.SetActive(false);
            _openWindowButton.onClick.AddListener(Open);
            _closeWindowButton.onClick.AddListener(Close);
        }
        
        private void Open()
        {
            _window.gameObject.SetActive(true);
        }
        
        private void Close()
        {
            _window.gameObject.SetActive(false);
        }
    }
}