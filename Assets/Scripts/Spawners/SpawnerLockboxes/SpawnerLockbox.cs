using System;
using System.Collections.Generic;
using UnityEngine;
using BaseElements.FolderLockbox;
using ColorService;
using LayersAndGroup;
using Player.InventorySystem;
using SavesDataSlot;
using Spawners.SpawnerKey;

namespace Spawners.SpawnerLockboxes
{
    public class SpawnerLockbox : BaseSpawner<Lockbox>
    {
        [SerializeField] private LockboxRegistry _lockboxRegistry;
        [SerializeField] private InactiveMarkerSpawner _inactiveMarkerSpawner;
        [SerializeField] private SlotDataStorage _slotDataManager;
        [SerializeField] [Range(1, 4)] private int _initialActivePoints = 1;
        
        private SpawnerKeys _keysSpawner;
        private KeyInventory _keyInventory;
        private KeyLayer _keyLayer;
        private LockboxSpawnPointsAvailability _spawnPointAvailability;
        private LockboxColorPicker _lockboxColorPicker;
        private ColorKeyTracker _colorKeyTracker;
        
        public event Action MaxSpawnPointsReached;
        
        public override void Awake()
        {
            base.Awake();
            
            _lockboxColorPicker = new LockboxColorPicker();
            _spawnPointAvailability = new LockboxSpawnPointsAvailability(SpawnPoints, _initialActivePoints, _slotDataManager.SavesDataKey);
        }
        
        private void OnEnable()
        {
            _lockboxRegistry.LockboxFilled += OnHandleFilled;
        }
        
        private void OnDisable()
        {
            _lockboxRegistry.LockboxFilled -= OnHandleFilled;
        }

        public override void Create()
        {
        }
        
        public void Initialize(SpawnerKeys keysSpawner, KeyInventory keyInventory, KeyLayer keyLayer)
        {
            _keysSpawner = keysSpawner;
            _keyInventory = keyInventory;
            _keyLayer = keyLayer;
            _colorKeyTracker = new ColorKeyTracker(_keysSpawner, _keyInventory);
            
            _inactiveMarkerSpawner.Clear();
            
            CreateInitial();
        }
        
        public bool IsMaxReached()
        {
            return _spawnPointAvailability.IsMaxReached();
        }
        
        public void PurchaseSlot()
        {
            Transform newSpawnPoint = _spawnPointAvailability.Purchase();
            
            if (newSpawnPoint != null)
            {
                _inactiveMarkerSpawner.Remove(newSpawnPoint);
                TryCreateNext(newSpawnPoint);
                
                _slotDataManager.Save();
                
                if (IsMaxReached())
                {
                    MaxSpawnPointsReached?.Invoke();
                }
            }
        }
        
        private void CreateInitial()
        {
            int requiredKeys = Prefab.RequiredKeys;
            
            List<Transform> activeSpawnPoints = _spawnPointAvailability.GetActive();
            List<Transform> inactiveSpawnPoints = _spawnPointAvailability.GetInactive();
            
            foreach (Transform spawnPoint in inactiveSpawnPoints)
            {
                _inactiveMarkerSpawner.Create(spawnPoint);
            }
            
            if (_colorKeyTracker.HasKeys())
            {
                foreach (Transform spawnPoint in activeSpawnPoints)
                {
                    BaseColors? newColor = _lockboxColorPicker.GetSingleForLayer(_keyLayer, _colorKeyTracker, requiredKeys);
                    
                    if (!newColor.HasValue)
                    {
                        break;
                    }
                    
                    CreateObject(spawnPoint, newColor.Value);
                }
            }
        }
        
        private void TryCreateNext(Transform spawnPoint)
        {
            int requiredKeys = Prefab.RequiredKeys;
            
            if (_colorKeyTracker.HasKeys())
            {
                BaseColors? newColor = _lockboxColorPicker.GetSingleForLayer(_keyLayer, _colorKeyTracker, requiredKeys);
                BaseColors? inventoryKeyColor = _keyInventory.GetRandomColor();
                
                if (newColor == null)
                {
                    newColor = inventoryKeyColor;
                }
                
                if (newColor.HasValue)
                {
                    CreateObject(spawnPoint, newColor.Value);
                }
            }
        }
        
        private void CreateObject(Transform spawnPoint, BaseColors color)
        {
            Lockbox lockbox = Pool.Get();
            
            lockbox.Initialize(color);
            _lockboxRegistry.Add(lockbox);
            
            SetTransform(lockbox, spawnPoint);
        }

        private void OnHandleFilled(Lockbox filledLockbox)
        {
            _lockboxRegistry.Remove(filledLockbox);
            Pool.Return(filledLockbox);
            filledLockbox.gameObject.SetActive(false);
            
            TryCreateNext(filledLockbox.transform);
        }
    }
}