using UnityEngine;

public abstract class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    public T Prefab;
    public Transform[] SpawnPoints;
    public int SizePool = 10;

    public BasePool<T> Pool;

    public virtual void Awake()
    {
        InitializePool();
    }

    public abstract void Spawn();

    private void InitializePool()
    {
        Pool = new BasePool<T>(Prefab, SizePool, transform);
    }

    public void SpawnByPoints()
    {
        int spawnCount = Mathf.Min(SizePool, SpawnPoints.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            T instance = Pool.Get();
            Transform spawnPoint = SpawnPoints[i];

            SetInstanceTransform(instance, spawnPoint);
        }
    }

    public void SetInstanceTransform(T instance, Transform spawnPoint)
    {
        instance.transform.position = spawnPoint.position;
        instance.transform.rotation = spawnPoint.rotation;
        instance.transform.localScale = spawnPoint.localScale;
    }
}