using System.Collections.Generic;
using UnityEngine;

public class SpawnerKeys : BaseSpawner<Key>
{
    [SerializeField] private KeySpawnConfig _keySpawnConfig;

    private SpawnIndexSelector _spawnIndexSelector;
    private List<Key> _activeKeys = new List<Key>();

    public override void Awake()
    {
        base.Awake();
        _spawnIndexSelector = new SpawnIndexSelector();
    }

    public override void Spawn()
    {
        Create();
    }

    public IEnumerable<Key> GetActiveKeys()
    {
        return _activeKeys;
    }

    private void Create()
    {
        _activeKeys.Clear();

        foreach (var layer in _keySpawnConfig.KeyLayers)
        {
            CreateKeysForLayer(layer);
        }
    }

    private void CreateKeysForLayer(KeyLayer layer)
    {
        int totalKeys = 0;

        foreach (var colorKeyCount in layer.ColorKeyCount)
        {
            totalKeys += colorKeyCount.Count;
        }

        if (totalKeys > SpawnPoints.Length)
        {
            totalKeys = SpawnPoints.Length;
        }

        List<int> spawnIndices = _spawnIndexSelector.GetIndices(totalKeys, totalKeys, SpawnPoints.Length);

        foreach (var colorKeyCount in layer.ColorKeyCount)
        {
            SpawnKeysOfColor(colorKeyCount.Color, colorKeyCount.Count, spawnIndices);
        }
    }

    private void SpawnKeysOfColor(BaseColor color, int amount, List<int> spawnIndices)
    {
        if (spawnIndices.Count < amount)
        {
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, spawnIndices.Count);
            int spawnIndex = spawnIndices[randomIndex];

            spawnIndices.RemoveAt(randomIndex);

            Key key = Pool.Get();
            Transform spawnPoint = SpawnPoints[spawnIndex];

            SetInstanceTransform(key, spawnPoint);
            key.Initialize(color);
            _activeKeys.Add(key);
        }
    }
}