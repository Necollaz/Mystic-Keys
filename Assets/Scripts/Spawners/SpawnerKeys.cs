using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerKeys : BaseSpawner<Key>
{
    [SerializeField] private KeyLayer _keyLayer;
    [SerializeField] private ColorKeyCount[] _keyCounts;

    private Dictionary<BaseColor, int> _remainingColorCounts;
    private List<Key> _activeKeys = new List<Key>();

    public override void Awake()
    {
        base.Awake();
        _remainingColorCounts = _keyCounts.ToDictionary(
            colorKeyCount => colorKeyCount.Color,
            colorKeyCount => colorKeyCount.Count
            );

        _keyLayer.LayerAdvanced += OnLayerAdvanced;
    }

    private void OnDestroy()
    {
        _keyLayer.LayerAdvanced -= OnLayerAdvanced;
    }

    public override void Spawn()
    {
        Create();
    }

    public List<Key> GetActiveKeys()
    {
        return _activeKeys;
    }

    private void OnLayerAdvanced()
    {
        Create();
    }

    private void Create()
    {
        int layerIndex = _keyLayer.CurrentLayerIndex;

        if (layerIndex < _keyLayer.Layers.Length)
        {
            LayerInfo layer = _keyLayer.GetCurrentLayer();

            CreateForLayer(layer, layerIndex);
        }
    }

    private void CreateForLayer(LayerInfo layer, int layerIndex)
    {
        int totalSpawnPoints = layer.SpawnPoints.Length;
        List<int> availableSpawnIndices = Enumerable.Range(0, totalSpawnPoints).ToList();

        foreach (KeyValuePair<BaseColor, int> colorKeyCount in _remainingColorCounts.Where(kvp => kvp.Value > 0).ToList())
        {
            BaseColor color = colorKeyCount.Key;
            int remainingCount = colorKeyCount.Value;

            if (availableSpawnIndices.Count > 0)
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

            int randomIndex = Random.Range(0, availableSpawnIndices.Count);
            int spawnIndex = availableSpawnIndices[randomIndex];
            availableSpawnIndices.RemoveAt(randomIndex);

            Key key = Pool.Get();
            Transform spawnPoint = spawnPoints[spawnIndex];

            SetInstanceTransform(key, spawnPoint);

            key.Initialize(color);
            key.LayerIndex = layerIndex;
            _keyLayer.Register(layerIndex, key);
            _activeKeys.Add(key);
        }
    }
}