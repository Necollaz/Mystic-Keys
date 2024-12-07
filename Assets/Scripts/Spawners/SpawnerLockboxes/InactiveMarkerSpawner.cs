using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners.SpawnerLockboxes
{
    [Serializable]
    public class InactiveMarkerSpawner
    {
        [SerializeField] private ParticleSystem _inactivePrefab;

        private Dictionary<Transform, ParticleSystem> _markers = new Dictionary<Transform, ParticleSystem>();

        public void CreateInactiveMarker(Transform point)
        {
            ParticleSystem marker = ParticleSystem.Instantiate(_inactivePrefab, point.position, point.rotation);
            _markers.Add(point, marker);
        }

        public void Remove(Transform spawnPoint)
        {
            if (_markers.TryGetValue(spawnPoint, out ParticleSystem marker))
            {
                marker.gameObject.SetActive(false);
                _markers.Remove(spawnPoint);
            }
        }

        public void Clear()
        {
            foreach (ParticleSystem marker in _markers.Values)
            {
                marker.gameObject.SetActive(false);
            }

            _markers.Clear();
        }
    }
}