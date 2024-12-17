using System;
using System.Collections.Generic;
using BaseElements.FolderLockbox;
using UnityEngine;

namespace Spawners.SpawnerLockboxes
{
    [Serializable]
    public class InactiveMarkerSpawner
    {
        [SerializeField] private LockboxMarker _marker;

        private Dictionary<Transform, LockboxMarker> _markers = new Dictionary<Transform, LockboxMarker>();

        public void Create(Transform point)
        {
            LockboxMarker marker = LockboxMarker.Instantiate(_marker, point.position, point.rotation);
            _markers.Add(point, marker);
        }

        public void Remove(Transform spawnPoint)
        {
            if (_markers.TryGetValue(spawnPoint, out LockboxMarker marker))
            {
                marker.gameObject.SetActive(false);
                _markers.Remove(spawnPoint);
            }
        }

        public void Clear()
        {
            foreach (LockboxMarker marker in _markers.Values)
            {
                marker.gameObject.SetActive(false);
            }

            _markers.Clear();
        }
    }
}