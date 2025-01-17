using System;
using UnityEngine;
using Player.InventorySystem;
using Spawners.SpawnerLockboxes;
using YG;

namespace Levels.UnlockSlots
{
    public class PurshedSlots : MonoBehaviour
    {
        [SerializeField] private ButtonInitializer _buttonInitializer;
        [SerializeField] private SpawnerLockbox _spawnerLockbox;
        [SerializeField] private Inventory _inventory;
        
        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += Reward;
            _spawnerLockbox.MaxSpawnPointsReached += OnMaxSpawnPointsReachedLockbox;
            _inventory.MaxSpawnPointsReached += OnMaxSpawnPointsReachedInventory;
        }
        
        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= Reward;
            _spawnerLockbox.MaxSpawnPointsReached -= OnMaxSpawnPointsReachedLockbox;
            _inventory.MaxSpawnPointsReached -= OnMaxSpawnPointsReachedInventory;
        }
        
        public void Start()
        {
            _buttonInitializer.SetupButtons(OpenReward);
            
            UpdateButtons();
        }
        
        private void Reward(int id)
        {
            if (id == 1)
            {
                _spawnerLockbox.PurchaseSlot();
            }
            else if (id == 2)
            {
                _inventory.BuyingSlot();
            }
        }
        
        private void OpenReward(int id)
        {
            YandexGame.RewVideoShow(id);
        }
        
        private void UpdateButtons()
        {
            UpdateButtonState(condition: _spawnerLockbox.IsMaxReached, showAction: _buttonInitializer.ShowUnlockLockboxButton, hideAction: _buttonInitializer.HideUnlockLockboxButton);
            
            UpdateButtonState(condition: _inventory.IsMaxReached, showAction: _buttonInitializer.ShowUnlockSlotButton, hideAction: _buttonInitializer.HideUnlockSlotButton);
        }
        
        private void UpdateButtonState(Func<bool> condition, Action showAction, Action hideAction)
        {
            if (condition())
            {
                hideAction();
            }
            else
            {
                showAction();
            }
        }
        
        private void OnMaxSpawnPointsReachedLockbox()
        {
            _buttonInitializer.HideUnlockLockboxButton();
        }
        
        private void OnMaxSpawnPointsReachedInventory()
        {
            _buttonInitializer.HideUnlockSlotButton();
        }
    }
}