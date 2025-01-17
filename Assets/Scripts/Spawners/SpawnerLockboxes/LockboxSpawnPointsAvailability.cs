using System.Collections.Generic;
using UnityEngine;
using SavesDataSlot;

namespace Spawners.SpawnerLockboxes
{
    public class LockboxSpawnPointsAvailability
    {
        private readonly SavesData _savesData;
        private readonly Transform[] _spawnPoints;
        private readonly List<int> _activeSpawnPointIndexes = new List<int>();
        
        private readonly int _initialActivePoints;
        private readonly int _maxActivePoints = 4;
        
        public LockboxSpawnPointsAvailability(Transform[] spawnPoints, int initialActivePoints, SavesData savesData)
        {
            _spawnPoints = spawnPoints;
            _initialActivePoints = initialActivePoints;
            _savesData = savesData;
            
            Initialize();
        }
        
        public bool IsMaxReached()
        {
            return _activeSpawnPointIndexes.Count >= _maxActivePoints;
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
        
        public Transform Purchase()
        {
            if (_activeSpawnPointIndexes.Count >= _maxActivePoints)
            {
                return null;
            }
            
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                if (!_activeSpawnPointIndexes.Contains(i))
                {
                    _activeSpawnPointIndexes.Add(i);
                    _savesData.PurchasedLockboxSlots.Add(i);
                    
                    return _spawnPoints[i];
                }
            }
            
            return null;
        }
        
        private void Initialize()
        {
            _activeSpawnPointIndexes.Clear();
            
            foreach (int index in _savesData.PurchasedLockboxSlots)
            {
                if (!_activeSpawnPointIndexes.Contains(index) && index >= 0 && index < _spawnPoints.Length)
                {
                    _activeSpawnPointIndexes.Add(index);
                }
            }
            
            if (_activeSpawnPointIndexes.Count == 0 && _spawnPoints.Length > 0)
            {
                int activeCount = Mathf.Clamp(_initialActivePoints, 1, _spawnPoints.Length);
                
                for (int i = 0; i < activeCount; i++)
                {
                    _savesData.PurchasedLockboxSlots.Add(i);
                    _activeSpawnPointIndexes.Add(i);
                }
            }
        }
    }
}