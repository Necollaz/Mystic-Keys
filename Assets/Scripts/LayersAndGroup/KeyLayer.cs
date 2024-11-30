using System.Collections.Generic;
using UnityEngine;

public class KeyLayer : MonoBehaviour
{
    [SerializeField] private LayerInfo[] _layers;

    private Dictionary<int, HashSet<Key>> _activeKeysPerLayer = new Dictionary<int, HashSet<Key>>();

    public LayerInfo[] Layers => _layers;
    public int CurrentLayerIndex { get; private set; } = 0;

    public bool IsCurrent(int layerIndex)
    {
        return layerIndex == CurrentLayerIndex;
    }

    //public HashSet<Key> GetActiveKeys(int layerIndex)
    //{
    //    if (_activeKeysPerLayer.TryGetValue(layerIndex, out var keys))
    //    {
    //        return keys;
    //    }
    //    return new HashSet<Key>();
    //}

    public void Register(int layerIndex, Key key)
    {
        if(_activeKeysPerLayer.ContainsKey(layerIndex) == false)
        {
            _activeKeysPerLayer[layerIndex] = new HashSet<Key>();
        }

        _activeKeysPerLayer[layerIndex].Add(key);
        key.SetInteractivity(IsCurrent(layerIndex));
    }

    public void Unregister(int layerIndex, Key key)
    {
        if (_activeKeysPerLayer.ContainsKey(layerIndex))
        {
            _activeKeysPerLayer[layerIndex].Remove(key);

            if (_activeKeysPerLayer[layerIndex].Count == 0)
            {
                _activeKeysPerLayer.Remove(layerIndex);
                CurrentLayerIndex++;

                EnableCurrentLayer();
            }
        }
    }

    private void EnableCurrentLayer()
    {
        if (CurrentLayerIndex < _layers.Length)
        {
            if (_activeKeysPerLayer.TryGetValue(CurrentLayerIndex, out HashSet<Key> newLayerKeys))
            {
                foreach (Key key in newLayerKeys)
                {
                    key.SetInteractivity(true);
                }
            }
        }
    }



    public Dictionary<BaseColor, int> GetKeysForLayer(int layerIndex)
    {
        Dictionary<BaseColor, int> keyCounts = new Dictionary<BaseColor, int>();

        for (int i = layerIndex; i < _layers.Length; i++)
        {
            if (_activeKeysPerLayer.TryGetValue(i, out HashSet<Key> keys))
            {
                foreach (Key key in keys)
                {
                    BaseColor color = key.Color;

                    if (keyCounts.ContainsKey(color))
                    {
                        keyCounts[color]++;
                    }
                    else
                    {
                        keyCounts[color] = 1;
                    }
                }
            }

            if (keyCounts.Count > 0)
            {
                break;
            }
        }

        return keyCounts;
    }
}