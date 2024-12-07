using System;
using UnityEngine;
using UnityEngine.UI;

namespace Levels
{
    [Serializable]
    public class ButtonInitializer
    {
        [SerializeField] private Button _unlockLockboxButton;
        [SerializeField] private Button _unlockSlotInventoryButton;

        public void SetupButtons(Action<int> onButtonClicked)
        {
            _unlockLockboxButton.onClick.AddListener(() => onButtonClicked?.Invoke(1));
            _unlockSlotInventoryButton.onClick.AddListener(() => onButtonClicked?.Invoke(2));
        }

        public void HideUnlockLockboxButton()
        {
            _unlockLockboxButton.gameObject.SetActive(false);
        }

        public void HideUnlockSlotButton()
        {
            _unlockSlotInventoryButton.gameObject.SetActive(false);
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