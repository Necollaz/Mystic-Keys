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
}