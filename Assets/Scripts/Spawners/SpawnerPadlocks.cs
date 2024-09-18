using UnityEngine;

public class SpawnerPadlocks : BaseSpawner<Padlock>
{
    private void Start()
    {
        Spawn();
    }

    public override void Spawn()
    {
        if (_spawnPoints == null || _spawnPoints.Length == 0) return;

        int spawnCount = Mathf.Min(_initialSize, _spawnPoints.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            Padlock padlock = _pool.Get();
            Transform spawnPoint = _spawnPoints[i];

            padlock.transform.position = spawnPoint.position;
            padlock.transform.rotation = spawnPoint.rotation;
            padlock.transform.localScale = spawnPoint.localScale;
        }
    }
}