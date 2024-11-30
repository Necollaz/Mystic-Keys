using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;
    [SerializeField] private LevelEndMenu _levelEndMenu;
    [SerializeField] private Transform _levelSpawnPoint;
    [SerializeField] private List<LevelMessage> _level1Messages;
    [SerializeField] private List<LevelMessage> _level2Messages;

    [Inject] private DiContainer _container;

    private Level _currentLevelInstance;
    private List<Level> _levelInstances = new List<Level>();

    public int CurrentLevelIndex { get; private set; } = 0;

    private void Start()
    {
        CurrentLevelIndex = 0;
        LoadLevel(CurrentLevelIndex);
    }

    public void LoadLevel(int index)
    {
        if (_currentLevelInstance != null)
        {
            UnsubscribeLevelEvents(_currentLevelInstance);
            _currentLevelInstance.gameObject.SetActive(false);
        }

        if (index >= 0 && index < _levels.Count)
        {
            Level levelPrefab = _levels[index];
            Vector3 spawnPosition = _levelSpawnPoint != null ? _levelSpawnPoint.position : Vector3.zero;
            _currentLevelInstance = _container.InstantiatePrefabForComponent<Level>(levelPrefab.gameObject, spawnPosition, Quaternion.identity, null);


            _container.InjectGameObject(_currentLevelInstance.gameObject);


            _currentLevelInstance.Initialize(this);

            if (_levelInstances.Count <= index)
            {
                _levelInstances.Add(_currentLevelInstance);
            }
            else
            {
                _levelInstances[index] = _currentLevelInstance;
            }

            SpawnerKeys spawnerKeys = _currentLevelInstance.SpawnerKeysInstance;
            spawnerKeys.AllKeysCollected += OnLevelCompleted;
            ShowLevelMessage(index);
        }
    }

    private void ShowLevelMessage(int levelIndex)
    {
        DeactivateAllLevelMessages();

        if (levelIndex == 0 && _level1Messages != null)
        {
            foreach (LevelMessage message in _level1Messages)
            {
                message.gameObject.SetActive(true);
            }
        }
        else if (levelIndex == 1 && _level2Messages != null)
        {
            foreach (LevelMessage message in _level2Messages)
            {
                message.gameObject.SetActive(true);
            }
        }
    }

    private void DeactivateAllLevelMessages()
    {
        foreach (var message in _level1Messages)
        {
            message.gameObject.SetActive(false);
        }
        foreach (var message in _level2Messages)
        {
            message.gameObject.SetActive(false);
        }
    }

    private void UnsubscribeLevelEvents(Level level)
    {
        SpawnerKeys spawnerKeys = level.SpawnerKeysInstance;

        if (spawnerKeys != null)
        {
            spawnerKeys.AllKeysCollected -= OnLevelCompleted;
        }
    }

    private void OnLevelCompleted()
    {
        _levelEndMenu.ShowLevelCompletedWindow();
    }

    public void ContinueToNextLevel()
    {
        CurrentLevelIndex++;
        LoadLevel(CurrentLevelIndex);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}