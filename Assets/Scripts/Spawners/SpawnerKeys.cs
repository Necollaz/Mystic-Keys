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

    private void Start()
    {
        Spawn();
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
        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogWarning("Точки спавна отсутствуют");
            return;
        }


        int totalKeys = _keysPerColor * _availableColors.Length;

        if (totalKeys > _spawnPoints.Length)
        {
            Debug.LogWarning("Недостаточно точек спавна для всех ключей");
            return;
        }


        List<int> spawnIndices = new List<int>();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            spawnIndices.Add(i);
        }

        foreach (BaseColor color in _availableColors)
        {
            Debug.Log("Спавн ключей для цвета: " + color);

            SpawnKeysOfColor(color, _keysPerColor, spawnIndices);
        }

        Debug.Log("Активные цвета после спавна: " + string.Join(", ", _activeColors));

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

            Debug.Log($"Ключ цвета {color} спавнен на позиции {spawnIndex}");
        }
    }
}