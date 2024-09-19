using UnityEngine;

public abstract class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected Transform[] _spawnPoints;
    [SerializeField] protected int _initialSize = 10;

    protected BasePool<T> _pool;

    protected virtual void Awake()
    {
        InitializePool();
    }

    public abstract void Spawn();

    private void InitializePool()
    {
        _pool = new BasePool<T>(_prefab, _initialSize, transform);
    }

    protected void DefaultSpawn()
    {
        if (_spawnPoints == null || _spawnPoints.Length == 0) return;

        int spawnCount = Mathf.Min(_initialSize, _spawnPoints.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            T instance = _pool.Get();

            if (instance == null) continue;

            Transform spawnPoint = _spawnPoints[i];
            instance.transform.position = spawnPoint.position;
            instance.transform.rotation = spawnPoint.rotation;
            instance.transform.localScale = spawnPoint.localScale;
        }
    }
}