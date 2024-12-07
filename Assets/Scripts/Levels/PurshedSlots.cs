using Player.InventorySystem;
using Spawners.SpawnerLockboxes;
using UnityEngine;
using YG;

namespace Levels
{
    public class PurshedSlots : MonoBehaviour
    {
        [SerializeField] private ButtonInitializer _buttonInitializer;
        [SerializeField] private SpawnerLockbox _spawnerLockbox;
        [SerializeField] private Inventory _inventory;

        public void Start()
        {
            _buttonInitializer.SetupButtons(OpenReward);

            UpdateButtons();
        }

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

        private void Reward(int id)
        {
            if(id == 1)
            {
                _spawnerLockbox.PurchaseSlot();
            }
            else if(id == 2)
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
            if (_spawnerLockbox.IsMaxReached())
            {
                _buttonInitializer.HideUnlockLockboxButton();
            }
            else
            {
                _buttonInitializer.ShowUnlockLockboxButton();
            }

            if (_inventory.IsMaxReached())
            {
                _buttonInitializer.HideUnlockSlotButton();
            }
            else
            {
                _buttonInitializer.ShowUnlockSlotButton();
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