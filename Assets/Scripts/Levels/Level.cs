using UnityEngine;

[RequireComponent(typeof(LaunchingSpawners))]
public class Level : MonoBehaviour
{
    private LevelLoader _levelLoader;
    private LaunchingSpawners _launchingSpawners;

    public SpawnerKeys SpawnerKeysInstance { get; private set; }

    public void Initialize(LevelLoader loader)
    {
        _levelLoader = loader;
        _launchingSpawners = GetComponent<LaunchingSpawners>();
        SpawnerKeysInstance = GetComponentInChildren<SpawnerKeys>();
    }
}