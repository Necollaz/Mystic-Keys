using UnityEngine;

public class LaunchingSpawners : MonoBehaviour
{
    [SerializeField] private SpawnerKeys _keysSpawner;
    [SerializeField] private SpawnerLockbox _lockboxSpawner;
    [SerializeField] private SpawnerPadlock _padlocksSpawner;
    [SerializeField] private SpawnerChisels _chiselsSpawner;
    [SerializeField] private SpawnerBeams _beamsSpawner;
    [SerializeField] private SpawnerDoor _doorSpawner;

    private void Start()
    {
        _doorSpawner.Create();
        _keysSpawner.Create();
        _padlocksSpawner.Create();
        _chiselsSpawner.Create();
        _beamsSpawner.Create();
        _lockboxSpawner.Create();
    }
}