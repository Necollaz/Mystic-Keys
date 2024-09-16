using System.Collections.Generic;
using UnityEngine;

public class BaseElementsPool : MonoBehaviour
{
    [SerializeField] private Key[] _keys;
    [SerializeField] private Padlock[] _padlocks;
    [SerializeField] private Beam[] _beams;
    [SerializeField] private Chisel[] _chisels;

    private Queue<Key> _keyPool = new Queue<Key>();
    private Queue<Padlock> _padlockPool = new Queue<Padlock>();
    private Queue<Chisel> _chiselPool = new Queue<Chisel>();
    private Queue<Beam> _beamPool = new Queue<Beam>();

    private void Awake()
    {
        PopulatePool(_keys, _keyPool);
        PopulatePool(_padlocks, _padlockPool);
        PopulatePool(_chisels, _chiselPool);
        PopulatePool(_beams, _beamPool);
    }

    public void Return<T>(T element, Queue<T> pool) where T : MonoBehaviour
    {
        element.gameObject.SetActive(false);
        pool.Enqueue(element);
    }

    public Key GetKey()
    {
        return Get(_keyPool);
    }

    public Padlock GetPadlock()
    {
        return Get(_padlockPool);
    }

    public Beam GetBeam()
    {
        return Get(_beamPool);
    }

    public Chisel GetChisel()
    {
        return Get(_chiselPool);
    }

    private void PopulatePool<T>(T[] objects, Queue<T> pool) where T : MonoBehaviour
    {
        foreach (var element in objects)
        {
            var instance = Instantiate(element);
            instance.gameObject.SetActive(false);
            pool.Enqueue(instance);
        }
    }

    private T Get<T>(Queue<T> pool) where T : MonoBehaviour
    {
        if(pool.Count > 0)
        {
            var element = pool.Dequeue();
            element.gameObject.SetActive(true);
            return element;
        }

        return null;
    }

}
