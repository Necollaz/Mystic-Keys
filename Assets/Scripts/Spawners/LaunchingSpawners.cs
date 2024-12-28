using Spawners.SpawnerKey;
using Spawners.SpawnerBeam;
using UnityEngine;

namespace Spawners
{
    public class LaunchingSpawners : MonoBehaviour
    {
        [SerializeField] private SpawnerKeys _keysSpawner;
        [SerializeField] private SpawnerPadlock _padlocksSpawner;
        [SerializeField] private SpawnerChisels _chiselsSpawner;
        [SerializeField] private SpawnerBeams _beamsSpawner;
        [SerializeField] private SpawnerDoor _doorSpawner;

        public SpawnerKeys KeysSpawner => _keysSpawner;

        public void InitializeCreation()
        {
            _doorSpawner.Create();
            _keysSpawner.Create();
            _padlocksSpawner.Create();
            _chiselsSpawner.Create();
            _beamsSpawner.Create();
        }
    }
}