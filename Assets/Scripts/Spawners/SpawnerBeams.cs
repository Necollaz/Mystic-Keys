using UnityEngine;

public class SpawnerBeams : BaseSpawner<Beam>
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
            Beam beam = _pool.Get();
            Transform spawnPoint = _spawnPoints[i];

            beam.transform.position = spawnPoint.position;
            beam.transform.rotation = spawnPoint.rotation;
            beam.transform.localScale = spawnPoint.localScale;
        }
    }
}