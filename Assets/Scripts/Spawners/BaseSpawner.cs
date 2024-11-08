using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Spawn by points")]
    public T Prefab;
    public Transform[] SpawnPoints;
    public BasePool<T> Pool;
    public int SizePool = 10;

    public List<T> SpawnedInstances { get; private set; } = new List<T>();

    public virtual void Awake()
    {
        InitializePool();
    }

    public virtual void OnInstanceCreated(T instance, int index) { }

    public virtual int GetGroupCount()
    {
        return 0;
    }

    public virtual Transform GetPoint(int index)
    {
        return null;
    }

    public abstract void Create();

    public void CreateByPoints()
    {
        int spawnCount = Mathf.Min(SizePool, SpawnPoints.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            T instance = Pool.Get();
            Transform spawnPoint = SpawnPoints[i];

            SetTransform(instance, spawnPoint);
            SpawnedInstances.Add(instance);
        }
    }

    public void CreateByGroups()
    {
        int spawnCount = Mathf.Min(SizePool, GetGroupCount());

        for (int i = 0; i < spawnCount; i++)
        {
            T instance = Pool.Get();
            Transform spawnPoint = GetPoint(i);

            SetTransform(instance, spawnPoint);
            SpawnedInstances.Add(instance);
            OnInstanceCreated(instance, i);
        }
    }

    public void SetTransform(T instance, Transform spawnPoint)
    {
        instance.transform.position = spawnPoint.position;
        instance.transform.rotation = spawnPoint.rotation;
        instance.transform.localScale = spawnPoint.localScale;
    }

    private void InitializePool()
    {
        Pool = new BasePool<T>(Prefab, SizePool, transform);
    }
}