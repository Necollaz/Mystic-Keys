using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerKeys : BaseSpawner<Key>
{
    [Header("Spawn by layer points")]
    [SerializeField] private KeyLayer _keyLayer;
    [SerializeField] private ColorKeyCount[] _keyCounts;
    [SerializeField] private SpawnGroupLocator _groups;

    private Dictionary<BaseColor, int> _remainingColorCounts;

    public event Action<Key> KeyCreated;

    public override void Awake()
    {
        base.Awake();
        _remainingColorCounts = _keyCounts.ToDictionary(
            colorKeyCount => colorKeyCount.Color,
            colorKeyCount => colorKeyCount.Count
            );
    }

    public override void Create()
    {
        CreateObject();
    }

    public Dictionary<BaseColor, int> GetActive()
    {
        Dictionary<BaseColor, int> keysPerColor = new Dictionary<BaseColor, int>();

        foreach (Key key in SpawnedInstances)
        {
            BaseColor color = key.Color;

            if (keysPerColor.ContainsKey(color))
            {
                keysPerColor[color]++;
            }
            else
            {
                keysPerColor[color] = 1;
            }
        }

        return keysPerColor;
    }

    private void CreateObject()
    {
        for (int layerIndex = 0; layerIndex < _keyLayer.Layers.Length; layerIndex++)
        {
            LayerInfo layer = _keyLayer.Layers[layerIndex];

            if (_remainingColorCounts.Values.Any(count => count > 0) == false)
            {
                break;
            }

            CreateForLayer(layer, layerIndex);
        }
    }

    private void CreateForLayer(LayerInfo layer, int layerIndex)
    {
        int totalSpawnPoints = layer.SpawnPoints.Length;
        List<int> availableSpawnIndices = Enumerable.Range(0, totalSpawnPoints).ToList();

        foreach (BaseColor color in _remainingColorCounts.Keys.ToList())
        {
            int remainingCount = _remainingColorCounts[color];

            if (remainingCount > 0 && availableSpawnIndices.Count > 0)
            {
                int amountToSpawn = Mathf.Min(remainingCount, availableSpawnIndices.Count);
                CreateOfColor(color, amountToSpawn, availableSpawnIndices, layer.SpawnPoints, layerIndex);
                _remainingColorCounts[color] -= amountToSpawn;
            }
        }
    }

    private void CreateOfColor(BaseColor color, int amount, List<int> availableSpawnIndices, Transform[] spawnPoints, int layerIndex)
    {
        for (int i = 0; i < amount; i++)
        {
            if (availableSpawnIndices.Count == 0)
            {
                break;
            }

            int randomIndex = UnityEngine.Random.Range(0, availableSpawnIndices.Count);
            int spawnIndex = availableSpawnIndices[randomIndex];
            availableSpawnIndices.RemoveAt(randomIndex);

            Key key = Pool.Get();
            Transform spawnPoint = spawnPoints[spawnIndex];

            SetTransform(key, spawnPoint);

            key.Initialize(color);
            key.LayerIndex = layerIndex;
            _keyLayer.Register(layerIndex, key);
            SpawnedInstances.Add(key);

            int groupIndex = _groups.FindGroupIndex(spawnPoint);
            key.GroupIndex = groupIndex;

            KeyCreated?.Invoke(key);
        }
    }
}