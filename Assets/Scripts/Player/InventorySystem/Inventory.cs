using System;
using BaseElements.FolderKey;
using BaseElements.FolderLockbox;
using SavesDataSlot;
using Spawners.SpawnerInventorySlot;
using Spawners.SpawnerLockboxes;
using UnityEngine;

namespace Player.InventorySystem
{
    [RequireComponent(typeof(MovemingKeys))]
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private InventorySpawnSlots _spawnSlots;
        [SerializeField] private LockboxRegistry _lockboxRegistry;
        [SerializeField] private SpawnerLockbox _spawnerLockbox;
        [SerializeField] private SlotDataStorage _slotDataManager;

        private SlotOrganizer _slotOrganizer;
        private KeyInventory _keyInventory;
        private MovemingKeys _movemingKeys;

        public event Action MaxSpawnPointsReached;

        public KeyInventory KeyInventory => _keyInventory;

        private void Awake()
        {   
            _movemingKeys = GetComponent<MovemingKeys>();
            _slotOrganizer = new SlotOrganizer(_spawnSlots, _slotDataManager.SavesDataKey, _slotDataManager);
            _keyInventory = new KeyInventory();
        }

        private void OnEnable()
        {
            _lockboxRegistry.LockboxCreated += OnLockboxCreated;
        }

        private void OnDisable()
        {
            _lockboxRegistry.LockboxCreated -= OnLockboxCreated;
        }

        private void Start()
        {
            _movemingKeys.Initialize(_keyInventory);
        }

        public bool AddKey(Key key)
        {
            return _keyInventory.Add(_slotOrganizer.GetActive(), key);
        }

        public bool HasSpace()
        {
            return _keyInventory.HasSpace(_slotOrganizer.GetActive());
        }

        public void BuyingSlot()
        {
            _slotOrganizer.ActivateNext();

            if (IsMaxReached())
            {
                MaxSpawnPointsReached?.Invoke();
            }

            _slotDataManager.Save();
        }

        public void InitializeSlots()
        {
            _slotOrganizer = new SlotOrganizer(_spawnSlots, _slotDataManager.SavesDataKey, _slotDataManager);
        }

        public bool IsMaxReached()
        {
            return _slotOrganizer.GetActive().Count >= _slotOrganizer.MaxActiveSlots;
        }

        private void OnLockboxCreated(Lockbox lockbox)
        {
            lockbox.Opened += _movemingKeys.OnLockboxOpened;
        }
    }
}