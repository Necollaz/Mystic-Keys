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
        return SpawnPoints != null ? SpawnPoints.Length : 0;
    }

    public virtual Transform GetPoint(int index)
    {
        if (SpawnPoints != null && index >= 0 && index < SpawnPoints.Length)
        {
            return SpawnPoints[index];
        }

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

            instance.transform.position = spawnPoint.position;
            instance.transform.rotation = spawnPoint.rotation;

            if (spawnPoint.TryGetComponent(out BeamSpawnPoint beamSpawnPoint))
            {
                if (beamSpawnPoint != null)
                {
                    Transform ModelTransform = instance.transform.Find("BeamModel");

                    if (ModelTransform != null)
                    {
                        ModelTransform.localScale = beamSpawnPoint.beamModelSize;
                        ModelTransform.localEulerAngles = beamSpawnPoint.beamModelEulerAngles;
                    }
                }
            }

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
    }

    private void InitializePool()
    {
        Pool = new BasePool<T>(Prefab, SizePool, transform);
    }
}