using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    private List<Key> _keys = new List<Key>();
    public IReadOnlyList<Key> Keys => _keys;

    public static KeyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void RegisterKey(Key key)
    {
        if (!_keys.Contains(key))
            _keys.Add(key);
    }

    public void UnregisterKey(Key key)
    {
        if (_keys.Contains(key))
            _keys.Remove(key);
    }
}