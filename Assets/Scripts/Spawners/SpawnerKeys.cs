using System.Collections.Generic;
using UnityEngine;

public class SpawnerKeys : BaseSpawner<Key>
{
    [SerializeField] private ColorKeyCount[] _keyCounts;
    [SerializeField] private SpawnLayer[] _spawnLayers;

    private SpawnIndexSelector _spawnIndexSelector;
    private HashSet<BaseColor> _activeColors = new HashSet<BaseColor>();

    public override void Awake()
    {
        base.Awake();
        _spawnIndexSelector = new SpawnIndexSelector();
    }

    public override void Spawn()
    {
        Create();
    }

    public IEnumerable<ColorKeyCount> GetActiveKeys()
    {
        return _keyCounts;
    }

    private void Create()
    {
        foreach (var layer in _spawnLayers)
        {
            CreateKeysForLayer(layer);
        }
    }

    private void CreateKeysForLayer(SpawnLayer layer)
    {
        int totalKeys = 0;

        foreach (var colorKeyCount in _keyCounts)
        {
            totalKeys += colorKeyCount.KeyCount;
        }

        if (totalKeys > layer.SpawnPoints.Length)
        {
            totalKeys = layer.SpawnPoints.Length;
        }

        List<int> spawnIndices = _spawnIndexSelector.GetIndices(totalKeys, totalKeys, layer.SpawnPoints.Length);

        foreach (var colorKeyCount in _keyCounts)
        {
            SpawnKeysOfColor(colorKeyCount.Color, colorKeyCount.KeyCount, spawnIndices, layer.SpawnPoints);
        }
    }

    private void SpawnKeysOfColor(BaseColor color, int amount, List<int> spawnIndices, Transform[] layerSpawnPoints)
    {
        for (int i = 0; i < amount; i++)
        {
            if (spawnIndices.Count == 0)
            {
                break;
            }

            int randomIndex = Random.Range(0, spawnIndices.Count);
            int spawnIndex = spawnIndices[randomIndex];

            spawnIndices.RemoveAt(randomIndex);

            Key key = Pool.Get();
            Transform spawnPoint = layerSpawnPoints[spawnIndex];

            SetInstanceTransform(key, spawnPoint);
            key.Initialize(color);
            _activeColors.Add(color);
        }
    }
}