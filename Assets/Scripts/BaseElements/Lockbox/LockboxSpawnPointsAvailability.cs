using System.Collections.Generic;
using UnityEngine;

public class LockboxSpawnPointsAvailability
{
    private Transform[] _spawnPoints;
    private List<int> _activeSpawnPointIndexes = new List<int>();

    public LockboxSpawnPointsAvailability(Transform[] spawnPoints)
    {
        _spawnPoints = spawnPoints;
        Initialize();
    }

    public void Purchase(int index)
    {
        if (_activeSpawnPointIndexes.Contains(index) == false && index >= 0 && index < _spawnPoints.Length)
        {
            _activeSpawnPointIndexes.Add(index);
        }
    }

    public List<Transform> GetActive()
    {
        List<Transform> activeSpawnPoints = new List<Transform>();

        foreach (int index in _activeSpawnPointIndexes)
        {
            activeSpawnPoints.Add(_spawnPoints[index]);
        }

        return activeSpawnPoints;
    }

    public List<Transform> GetInactive()
    {
        List<Transform> inactiveSpawnPoints = new List<Transform>();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            if (!_activeSpawnPointIndexes.Contains(i))
            {
                inactiveSpawnPoints.Add(_spawnPoints[i]);
            }
        }

        return inactiveSpawnPoints;
    }

    private void Initialize()
    {
        if (_spawnPoints.Length > 0)
        {
            _activeSpawnPointIndexes.Add(0);
        }
    }
}