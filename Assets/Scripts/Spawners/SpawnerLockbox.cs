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
        Debug.Log("Начало спавна Lockbox");
        if (_availableSpawnPoints == 0 || _currentSpawnIndex >= _availableSpawnPoints)
        {
            Debug.LogWarning("Нет доступных точек спавна или превышен индекс спавна");
            return;
        }


        BaseColor[] availableColors = _keysSpawner.GetActiveKey();

        if (availableColors.Length == 0)
        {
            Debug.LogWarning("Нет доступных цветов для ключей");
            return;
        }


        BaseColor randomColor = availableColors[Random.Range(0, availableColors.Length)];
        Lockbox lockbox = _lockboxPool.Get();

        if (lockbox == null)
        {
            Debug.LogWarning("Нет доступных объектов в пуле Lockbox");
            return;
        }


        Transform spawnPoint = _spawnPoints[_currentSpawnIndex];
        _currentSpawnIndex++;
        lockbox.transform.position = spawnPoint.position;
        lockbox.transform.rotation = spawnPoint.rotation;
        lockbox.transform.localScale = spawnPoint.localScale;
        lockbox.Initialize(randomColor);
        lockbox.OnLockboxFilled += Filled;
        Debug.Log("Успешный спавн Lockbox");

    }

    private void Filled(Lockbox lockbox)
    {
        lockbox.OnLockboxFilled -= Filled;
        _lockboxPool.Return(lockbox);
    }
}