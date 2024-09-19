using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerLockbox : BaseSpawner<Lockbox>
{
    [SerializeField] private SpawnerKeys _keysSpawner;
    [SerializeField] private int _availableSpawnPoints = 2;

    public List<BaseColor> SpawnedLockboxColors { get; private set; } = new List<BaseColor>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void UnlockPoint()
    {
        if (_availableSpawnPoints < _spawnPoints.Length)
            _availableSpawnPoints++;
    }

    public override void Spawn()
    {
        BaseColor[] availableColors = _keysSpawner.GetActiveKey();

        if (availableColors.Length == 0 || _availableSpawnPoints == 0) return;

        SpawnedLockboxColors.Clear();

        if (availableColors.Length == 0) return;

        int lockboxesToSpawn = Mathf.Min(availableColors.Length, _availableSpawnPoints);

        BaseColor[] selectedColors = availableColors.OrderBy(c => Random.value).Take(lockboxesToSpawn).ToArray();
        Transform[] availableSpawnPoints = _spawnPoints.Take(_availableSpawnPoints).ToArray();
        Transform[] selectedSpawnPoints = availableSpawnPoints.OrderBy(s => Random.value).Take(lockboxesToSpawn).ToArray();

        for (int i = 0; i < lockboxesToSpawn; i++)
        {
            BaseColor color = selectedColors[i];
            Transform spawnPoint = selectedSpawnPoints[i];
            Lockbox lockbox = _pool.Get();

            if (lockbox == null) return;

            lockbox.transform.position = spawnPoint.position;
            lockbox.transform.rotation = spawnPoint.rotation;
            lockbox.transform.localScale = spawnPoint.localScale;
            lockbox.Initialize(color);
            lockbox.OnLockboxFilled += Filled;

            SpawnedLockboxColors.Add(color);
        }
    }

    private void Filled(Lockbox lockbox)
    {
        lockbox.OnLockboxFilled -= Filled;
        _pool.Return(lockbox);
    }
}