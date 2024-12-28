using System.Collections.Generic;
using LayersAndGroup;
using Levels.LevelMessage;
using Menu.WindowLevel;
using Player.InventorySystem;
using SavesDataSlot;
using Spawners;
using Spawners.SpawnerKey;
using Spawners.SpawnerLockboxes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Transform _levelSpawnPoint;
        [SerializeField] private List<Level> _levels;
        [SerializeField] private SpawnerLockbox _spawnerLockbox;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private LevelEndMenu _levelEndMenu;
        [SerializeField] private LevelMessageDisplayer _levelMessageDisplayer;
        [SerializeField] private SlotDataStorage _slotDataStorage;

        private Level _currentLevelInstance;
        private ProgressDataStorage _progressDataStorage;

        private int _currentIndex;
        private int _lastRandomIndex = -1;
        private bool _isRandomPhase = false;

        public int CurrentIndex => _currentIndex;

        private void Start()
        {
            _progressDataStorage = new ProgressDataStorage();

            InitializeCurrentIndex();
            Load(_currentIndex);
        }

        public void Continue()
        {
            UnloadCurrent();

            if (!_isRandomPhase)
            {
                _currentIndex++;

                if (_currentIndex >= _levels.Count)
                {
                    _isRandomPhase = true;
                }
            }

            if (_isRandomPhase)
            {
                _currentIndex = GetRandomIndex();
            }

            SaveCurrentIndex();
            ReloadCurrentScene();
            Load(_currentIndex);
        }

        public void RestartAll()
        {
            _currentIndex = 0;
            _isRandomPhase = false;
            Time.timeScale = 1f;

            ResetPlayerProgress();
            SaveCurrentIndex();
            ReloadCurrentScene();
        }

        public void RestartCurrent()
        {
            _slotDataStorage.Save();
            _inventory.InitializeSlots();

            ReloadCurrentScene();
        }

        private void Load(int index)
        {
            UnloadCurrent();
            Create(index);
            InitializeComponents(_currentLevelInstance);
            ShowLevelMessage(index);
            _inventory.InitializeSlots();
        }

        private void UnloadCurrent()
        {
            if (_currentLevelInstance != null)
            {
                UnsubscribeEvents(_currentLevelInstance);
                Destroy(_currentLevelInstance.gameObject);
                _currentLevelInstance = null;
            }
        }

        private void Create(int index)
        {
            _currentLevelInstance = Instantiate(_levels[index], _levelSpawnPoint.position, Quaternion.identity);
        }

        private void InitializeComponents(Level level)
        {
            if (level.TryGetComponent(out LaunchingSpawners launchingSpawners))
            {
                launchingSpawners.InitializeCreation();
                SpawnerKeys spawnerKeys = launchingSpawners.KeysSpawner;
                KeyLayer keyLayer = level.GetComponentInChildren<KeyLayer>();
                KeyInventory keyInventory = _inventory.KeyInventory;
                _spawnerLockbox.Initialize(spawnerKeys, keyInventory, keyLayer);
                spawnerKeys.AllKeysCollected += OnCompleted;
            }
        }

        private void ShowLevelMessage(int index)
        {
            _levelMessageDisplayer.Show(index);
        }

        private void ResetPlayerProgress()
        {
            _progressDataStorage.Reset();
            _slotDataStorage.Reset();
        }

        private void SaveCurrentIndex()
        {
            _progressDataStorage.Save(_currentIndex, _isRandomPhase);
        }

        private void UnsubscribeEvents(Level level)
        {
            SpawnerKeys spawnerKeys = level.LaunchingSpawners?.KeysSpawner;

            if (spawnerKeys != null)
            {
                spawnerKeys.AllKeysCollected -= OnCompleted;
            }
        }

        private void InitializeCurrentIndex()
        {
            if (_progressDataStorage.HasSaved())
            {
                _currentIndex = _progressDataStorage.LoadLevelIndex();
                _isRandomPhase = _progressDataStorage.IsRandomLevelPhase();
            }
            else
            {
                _currentIndex = 0;
                _isRandomPhase = false;
            }
        }

        private void OnCompleted()
        {
            _levelEndMenu.Show();
        }

        private void ReloadCurrentScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }

        private int GetRandomIndex()
        {
            int randomIndex;
            int minIndex = 5;
            int maxIndex = _levels.Count;

            if (maxIndex <= minIndex)
            {
                return minIndex;
            }

            do
            {
                randomIndex = Random.Range(minIndex, maxIndex);
            }
            while (randomIndex == _lastRandomIndex && (maxIndex - minIndex) > 1);

            _lastRandomIndex = randomIndex;

            return randomIndex;
        }
    }
}