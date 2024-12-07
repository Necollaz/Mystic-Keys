using System.Collections.Generic;
using LayersAndGroup;
using Levels.LevelMessage;
using Menu.Level;
using Player.InventorySystem;
using ScriptableObjectSlot;
using Spawners;
using Spawners.SpawnerKey;
using Spawners.SpawnerLockboxes;
using UnityEngine;

namespace Levels
{
    public class LevelLoader : MonoBehaviour
    {
        private const string CurrentLevelIndex = "CurrentLevelIndex";

        [SerializeField] private Transform _levelSpawnPoint;
        [SerializeField] private List<Level> _levels;
        [SerializeField] private SpawnerLockbox _spawnerLockbox;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private LevelEndMenu _levelEndMenu;
        [SerializeField] private GameEndMenu _gameEndMenu;
        [SerializeField] private LevelMessageDisplayer _levelMessageDisplayer;
        [SerializeField] private SlotDataManager _slotDataManager;

        private SceneTransitionService _sceneTransitionService;
        private Level _currentLevelInstance;

        private int _currentLevelIndex;

        private void Awake()
        {
            _sceneTransitionService = new SceneTransitionService();
        }

        private void Start()
        {
            InitializeCurrentLevelIndex();
            LoadLevel(_currentLevelIndex);
        }

        public void LoadLevel(int index)
        {
            if (_currentLevelInstance != null)
            {
                UnsubscribeLevelEvents(_currentLevelInstance);
                Destroy(_currentLevelInstance.gameObject);
                _currentLevelInstance = null;
            }

            _currentLevelInstance = Instantiate(_levels[index], _levelSpawnPoint.position, Quaternion.identity);

            if (_currentLevelInstance.TryGetComponent(out LaunchingSpawners launchingSpawners))
            {
                launchingSpawners.InitializeCreation();

                SpawnerKeys spawnerKeys = launchingSpawners.KeysSpawner;
                KeyLayer keyLayer = _currentLevelInstance.GetComponentInChildren<KeyLayer>();
                KeyInventory keyInventory = _inventory.KeyInventory;

                _spawnerLockbox.Initialize(spawnerKeys, keyInventory, keyLayer);

                spawnerKeys.AllKeysCollected += OnLevelCompleted;
            }

            _levelMessageDisplayer.ShowLevelMessages(index);
        }

        public void ContinueToNextLevel()
        {
            if (_currentLevelInstance != null)
            {
                SpawnerKeys spawnerKeys = _currentLevelInstance.LaunchingSpawners?.KeysSpawner;
                spawnerKeys.AllKeysCollected -= OnLevelCompleted;
                _currentLevelInstance.gameObject.SetActive(false);
                _currentLevelInstance = null;
            }

            _currentLevelIndex++;

            if (_currentLevelIndex < _levels.Count)
            {
                SaveCurrentLevelIndex();
                _sceneTransitionService.ReloadCurrentScene();
                LoadLevel(_currentLevelIndex);
            }
            else
            {
                _gameEndMenu.Show();
            }
        }

        public void RestartAllLevels()
        {
            _currentLevelIndex = 0;

            ResetPlayerProgress();
            SaveCurrentLevelIndex();

            _sceneTransitionService.ReloadCurrentScene();
        }

        public void RestartCurrentLevel()
        {
            _sceneTransitionService.ReloadCurrentScene();
        }

        private void ResetPlayerProgress()
        {
            PlayerPrefs.DeleteKey(CurrentLevelIndex);
            _slotDataManager.Reset();
        }

        private void SaveCurrentLevelIndex()
        {
            PlayerPrefs.SetInt(CurrentLevelIndex, _currentLevelIndex);
        }

        private void UnsubscribeLevelEvents(Level level)
        {
            SpawnerKeys spawnerKeys = level.LaunchingSpawners?.KeysSpawner;

            spawnerKeys.AllKeysCollected -= OnLevelCompleted;
        }

        private void InitializeCurrentLevelIndex()
        {
            if (PlayerPrefs.HasKey(CurrentLevelIndex))
            {
                _currentLevelIndex = PlayerPrefs.GetInt(CurrentLevelIndex);
            }
            else
            {
                _currentLevelIndex = 0;
            }
        }

        private void OnLevelCompleted()
        {
            _levelEndMenu.Show();
        }
    }
}