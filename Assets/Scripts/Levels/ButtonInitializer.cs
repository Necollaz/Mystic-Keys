using System;
using UnityEngine;
using UnityEngine.UI;

namespace Levels
{
    [Serializable]
    public class ButtonInitializer
    {
        private const int _unlockLockbox = 1;
        private const int _unlockSlotInventory = 2;
        
        [SerializeField] private Button _unlockLockboxButton;
        [SerializeField] private Button _unlockSlotInventoryButton;
        [SerializeField] private Image _backgroundImageLockbox;
        [SerializeField] private Image _backgroundImageSlotInventory;
        
        public void SetupButtons(Action<int> onButtonClicked)
        {
            _unlockLockboxButton.onClick.AddListener(() => onButtonClicked?.Invoke(_unlockLockbox));
            _unlockSlotInventoryButton.onClick.AddListener(() => onButtonClicked?.Invoke(_unlockSlotInventory));
        }
        
        public void HideUnlockLockboxButton()
        {
            _unlockLockboxButton.gameObject.SetActive(false);
            _backgroundImageLockbox.gameObject.SetActive(false);
        }
        
        public void HideUnlockSlotButton()
        {
            _unlockSlotInventoryButton.gameObject.SetActive(false);
            _backgroundImageSlotInventory.gameObject.SetActive(false);
        }
        
        public void ShowUnlockLockboxButton()
        {
            _unlockLockboxButton.gameObject.SetActive(true);
        }
        
        public void ShowUnlockSlotButton()
        {
            _unlockSlotInventoryButton.gameObject.SetActive(true);
        }
    }
}