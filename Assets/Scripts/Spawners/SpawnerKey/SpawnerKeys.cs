using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ColorService;
using LayersAndGroup;
using BaseElements.FolderKey;
using YG;
using Player;

namespace Spawners.SpawnerKey
{
    public class SpawnerKeys : BaseSpawner<Key>
    {
        private const string NameLiderboard = "LeaderBoard";

        [Header("Spawn by layer points")]
        [SerializeField] private KeyLayer _keyLayer;
        [SerializeField] private ColorKeyCount[] _keyCounts;
        [SerializeField] private SpawnGroupLocator _groups;
        [SerializeField] private ScoreTracker _scoreTracker;

        private Dictionary<BaseColors, int> _remainingColorCounts;

        public event Action<Key> KeyCreated;
        public event Action AllKeysCollected;

        public int TotalKeys { get; private set; }

        public override void Awake()
        {
            base.Awake();

            _remainingColorCounts = _keyCounts.ToDictionary(colorKeyCount => colorKeyCount.Color, keyCount => keyCount.Count);
        }

        public override void Create()
        {
            TryCreateObjects();
        }

        public Dictionary<BaseColors, int> GetActive()
        {
            Dictionary<BaseColors, int> activeKeysByColor = new Dictionary<BaseColors, int>();

            foreach (Key key in SpawnedInstances)
            {
                if (key.IsPickedUp)
                {
                    continue;
                }

                BaseColors color = key.Color;

                if (!activeKeysByColor.ContainsKey(color))
                {
                    activeKeysByColor[color] = 0;
                }

                activeKeysByColor[color]++;
            }

            return activeKeysByColor;
        }

        private void TryCreateObjects()
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

            TotalKeys = SpawnedInstances.Count;
        }

        private void CreateForLayer(LayerInfo layer, int layerIndex)
        {
            int totalSpawnPoints = layer.SpawnPoints.Length;
            List<int> availableSpawnIndices = Enumerable.Range(0, totalSpawnPoints).ToList();

            foreach (BaseColors color in _remainingColorCounts.Keys.ToList())
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

        private void CreateOfColor(BaseColors color, int amount, List<int> availableSpawnIndices, Transform[] spawnPoints, int layerIndex)
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

                bool isCurrentLayer = layerIndex == _keyLayer.CurrentLayerIndex;

                key.SetInteractivity(isCurrentLayer);
                _keyLayer.Register(layerIndex, key);
                SpawnedInstances.Add(key);

                int groupIndex = _groups.FindGroupIndex(spawnPoint);
                key.GroupIndex = groupIndex;

                key.Collected += OnKeyCollected;
                key.Collected += _scoreTracker.Add;
                KeyCreated?.Invoke(key);
            }
        }

        private void OnKeyCollected(Key key)
        {
            key.Collected -= _scoreTracker.Add;
            key.Collected -= OnKeyCollected;

            _keyLayer.Unregister(key.LayerIndex, key);

            SpawnedInstances.Remove(key);
            Pool.Return(key);

            TotalKeys--;

            if (TotalKeys <= 0)
            {
                AllKeysCollected?.Invoke();

                if (YandexGame.SDKEnabled)
                {
                    if (YandexGame.auth)
                    {
                        YandexGame.NewLeaderboardScores(NameLiderboard, _scoreTracker.CurrentScore);
                    }
                }
            }
        }
    }
}