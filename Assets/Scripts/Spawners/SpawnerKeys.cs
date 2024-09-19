using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerKeys : BaseSpawner<Key>
{
    [SerializeField] private BaseColor[] _availableColors;
    [SerializeField] private int _keysPerColor = 3;

    private HashSet<BaseColor> _activeColors = new HashSet<BaseColor>();

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Spawn()
    {
        Create();
    }

    public BaseColor[] GetActiveKey()
    {
        return _activeColors.ToArray();
    }

    private void Create()
    {
        if (_spawnPoints == null || _spawnPoints.Length == 0) return;

        int totalKeys = _keysPerColor * _availableColors.Length;

        if (totalKeys > _spawnPoints.Length) return;


        List<int> spawnIndices = new List<int>();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            spawnIndices.Add(i);
        }

        foreach (BaseColor color in _availableColors)
        {
            SpawnKeysOfColor(color, _keysPerColor, spawnIndices);
        }
    }

    private void SpawnKeysOfColor(BaseColor color, int amount, List<int> spawnIndices)
    {
        for (int i = 0; i < amount; i++)
        {
            if (spawnIndices.Count == 0) return;

            int randomIndex = Random.Range(0, spawnIndices.Count);
            int spawnIndex = spawnIndices[randomIndex];

            spawnIndices.RemoveAt(randomIndex);

            Key key = _pool.Get();

            if (key == null) return;

            Transform spawnPoint = _spawnPoints[spawnIndex];
            key.transform.position = spawnPoint.position;
            key.transform.rotation = spawnPoint.rotation;
            key.transform.localScale = spawnPoint.localScale;
            key.Initialize(color);
            _activeColors.Add(color);
        }
    }
}