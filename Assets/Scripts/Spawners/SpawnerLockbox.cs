using UnityEngine;

public class SpawnerLockbox : BaseSpawner<Lockbox>
{
    [SerializeField] private SpawnerKeys _keysSpawner;
    [SerializeField] private PoolLockbox _lockboxPool;
    private int _availableSpawnPoints = 1;
    private int _currentSpawnIndex = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Spawn();
    }

    public void UnlockPoint()
    {
        if (_availableSpawnPoints < _spawnPoints.Length)
            _availableSpawnPoints++;
    }

    public override void Spawn()
    {
        if (_availableSpawnPoints == 0) return;

        BaseColor[] availableColors = _keysSpawner.TakeAvailable();

        if (availableColors.Length == 0) return;

        BaseColor randomColor = availableColors[Random.Range(0, availableColors.Length)];
        Lockbox lockbox = _pool.Get();
        Transform spawnPoint = _spawnPoints[_currentSpawnIndex];
        lockbox.transform.position = spawnPoint.position;
        lockbox.transform.rotation = spawnPoint.rotation;
        lockbox.transform.localScale = spawnPoint.localScale;
        lockbox.Initialize(randomColor);
        lockbox.OnLockboxFilled += Filled;

        _currentSpawnIndex = (_currentSpawnIndex + 1) % _availableSpawnPoints;
    }

    private void Filled(Lockbox lockbox)
    {
        lockbox.OnLockboxFilled -= Filled;
        _pool.Return(lockbox);
        Spawn();
    }
}