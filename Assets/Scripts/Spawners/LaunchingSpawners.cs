using UnityEngine;

public class LaunchingSpawners : MonoBehaviour
{
    [SerializeField] private SpawnerKeys _keysSpawner;
    [SerializeField] private SpawnerLockbox _lockboxSpawner;
    [SerializeField] private SpawnerPadlocks _padlocksSpawner;
    [SerializeField] private SpawnerChisels _chiselsSpawner;
    [SerializeField] private SpawnerBeams _beamsSpawner;
    [SerializeField] private SpawnerDoor _doorSpawner;

    private void Start()
    {
        _doorSpawner.Spawn();
        _keysSpawner.Spawn();
        _padlocksSpawner.Spawn();
        _chiselsSpawner.Spawn();
        _beamsSpawner.Spawn();
        _lockboxSpawner.Spawn();
    }
}