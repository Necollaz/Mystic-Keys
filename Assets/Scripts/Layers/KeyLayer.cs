using System.Collections.Generic;
using UnityEngine;

public class KeyLayer : MonoBehaviour
{
    [SerializeField] private LayerInfo[] _layers;

    private Dictionary<int, HashSet<Key>> _activeKeysPerLayer = new Dictionary<int, HashSet<Key>>();
    private int _currentLayerIndex = 0;

    public LayerInfo[] Layers => _layers;

    public bool IsCurrent(int layerIndex)
    {
        return layerIndex == _currentLayerIndex;
    }

    public void Register(int layerIndex, Key key)
    {
        if(_activeKeysPerLayer.ContainsKey(layerIndex) == false)
        {
            _activeKeysPerLayer[layerIndex] = new HashSet<Key>();
        }

        _activeKeysPerLayer[layerIndex].Add(key);
    }

    public void Unregister(int layerIndex, Key key)
    {
        if (_activeKeysPerLayer.ContainsKey(layerIndex))
        {
            _activeKeysPerLayer[layerIndex].Remove(key);

            if (_activeKeysPerLayer[layerIndex].Count == 0)
            {
                _activeKeysPerLayer.Remove(layerIndex);
                AdvanceToNext();
            }
        }
    }

    public bool IsCleared(int layerIndex)
    {
        return _activeKeysPerLayer.ContainsKey(layerIndex) == false || _activeKeysPerLayer[layerIndex].Count == 0;
    }

    private void AdvanceToNext()
    {
        _currentLayerIndex++;
    }
}