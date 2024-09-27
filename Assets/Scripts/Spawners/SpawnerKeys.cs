using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerKeys : BaseSpawner<Key>
{
    [SerializeField] private ColorKeyCount[] _keyCounts;

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
        int currentLayer = 0;
        while (_keyCounts.Any(cc => cc.Layer == currentLayer))
        {
            CreateKeysForLayer(currentLayer);
            currentLayer++;
        }
    }

    private void CreateKeysForLayer(int layer)
    {
        var keysInLayer = _keyCounts.Where(cc => cc.Layer == layer).ToArray();
        int totalKeys = keysInLayer.Sum(keyCount => keyCount.KeyCount);

        if (totalKeys > SpawnPoints.Length)
        {
            totalKeys = SpawnPoints.Length;
        }

        List<int> spawnIndices = _spawnIndexSelector.GetIndices(totalKeys, totalKeys, SpawnPoints.Length);

        foreach (ColorKeyCount colorKeyCount in keysInLayer)
        {
            SpawnKeysOfColor(colorKeyCount.Color, colorKeyCount.KeyCount, spawnIndices);
        }
    }

    private void SpawnKeysOfColor(BaseColor color, int amount, List<int> spawnIndices)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, spawnIndices.Count);
            int spawnIndex = spawnIndices[randomIndex];
            spawnIndices.RemoveAt(randomIndex);
            Key key = Pool.Get();
            Transform spawnPoint = SpawnPoints[spawnIndex];
            SetInstanceTransform(key, spawnPoint);
            key.Initialize(color);
            _activeColors.Add(color);
        }
    }
}