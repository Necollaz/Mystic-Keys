using System.Collections.Generic;
using UnityEngine;

public class SpawnerKeys : BaseSpawner<Key>
{
    [SerializeField] private BaseColor[] _availableColors;
    [SerializeField] private int _keysPerColor = 3;

    private List<int> _spawnIndices;

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
        SpawnKeys();
    }

    public BaseColor[] TakeAvailable()
    {
        return _availableColors;
    }

    private void SpawnKeys()
    {
        int totalKeys = _keysPerColor * _availableColors.Length;

        if (totalKeys > _spawnPoints.Length) return;

        PrepareIndices();

        foreach (BaseColor color in _availableColors)
        {
            SpawnKeysOfColor(color, _keysPerColor);
        }
    }

    private void PrepareIndices()
    {
        _spawnIndices = new List<int>();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnIndices.Add(i);
        }

        Shuffle(_spawnIndices);
    }

    private void SpawnKeysOfColor(BaseColor color, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (_spawnIndices.Count == 0) return;

            int spawnIndex = _spawnIndices[0];
            _spawnIndices.RemoveAt(0);

            Key key = _pool.Get();
            Transform spawnPoint = _spawnPoints[spawnIndex];

            if (key == null) return;

            key.transform.position = spawnPoint.position;
            key.transform.rotation = spawnPoint.rotation;
            key.transform.localScale = spawnPoint.localScale;
            key.Initialize(color);
        }
    }

    private void Shuffle<T>(IList<T> list)
    {
        int listCount = list.Count;

        while (listCount > 1)
        {
            listCount--;
            int randomIndex = Random.Range(0, listCount + 1);
            T tempItem = list[randomIndex];
            list[randomIndex] = list[listCount];
            list[listCount] = tempItem;
        }
    }
}