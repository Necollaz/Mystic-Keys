using System.Collections.Generic;
using UnityEngine;

public class BasePool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Transform _parent;
    private Queue<T> _pool;

    public BasePool(T prefab, int initialSize, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;
        _pool = new Queue<T>();

        for (int i = 0; i < initialSize; i++)
        {
            T instance = CreateInstance();
            Return(instance);
        }
    }

    public T Get()
    {
        if (_pool.Count > 0)
        {
            T instance = _pool.Dequeue();
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            return CreateInstance();
        }
    }

    public void Return(T instance)
    {
        instance.gameObject.SetActive(false);
        _pool.Enqueue(instance);
    }

    private T CreateInstance()
    {
        T instance = Object.Instantiate(_prefab, _parent);
        instance.gameObject.SetActive(false);
        return instance;
    }
}