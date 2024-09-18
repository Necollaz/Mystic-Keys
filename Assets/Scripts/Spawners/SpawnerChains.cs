using UnityEngine;

public class SpawnerChains : BaseSpawner<Chain>
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
            Chain chain = _pool.Get();
            Transform spawnPoint = _spawnPoints[i];

            chain.transform.position = spawnPoint.position;
            chain.transform.rotation = spawnPoint.rotation;
            chain.transform.localScale = spawnPoint.localScale;
        }
    }
}