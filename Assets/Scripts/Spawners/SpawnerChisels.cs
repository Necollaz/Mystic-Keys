using UnityEngine;

public class SpawnerChisels : BaseSpawner<Chisel>
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
            Chisel chisel = _pool.Get();

            if (chisel == null) continue;

            Transform spawnPoint = _spawnPoints[i];
            chisel.transform.position = spawnPoint.position;
            chisel.transform.rotation = spawnPoint.rotation;
            chisel.transform.localScale = spawnPoint.localScale;
        }
    }
}