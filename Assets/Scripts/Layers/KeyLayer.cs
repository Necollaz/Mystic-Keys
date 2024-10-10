using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyLayer : MonoBehaviour
{
    [SerializeField] private LayerInfo[] _layers;

    private Dictionary<int, HashSet<Key>> _activeKeysPerLayer = new Dictionary<int, HashSet<Key>>();

    public LayerInfo[] Layers => _layers;
    public int CurrentLayerIndex
    {
        get { return CurrentLayerIndex; }
        private set { CurrentLayerIndex = 0; }
    }

    public event Action LayerAdvanced;

    public LayerInfo GetCurrentLayer()
    {
        if(CurrentLayerIndex < _layers.Length)
        {
            return _layers[CurrentLayerIndex];
        }
        else
        {
            return null;
        }
    }

    public bool IsCurrentLayer(int layerIndex)
    {
        return layerIndex == CurrentLayerIndex;
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
                if (layerIndex == CurrentLayerIndex)
                {
                    AdvanceToNextLayer();
                }
            }
        }
    }

    public bool IsCleared(int layerIndex)
    {
        return _activeKeysPerLayer.ContainsKey(layerIndex) == false || _activeKeysPerLayer[layerIndex].Count == 0;
    }

    public bool AllCleared()
    {
        return CurrentLayerIndex >= _layers.Length;
    }

    private void AdvanceToNextLayer()
    {
        CurrentLayerIndex++;

        if (CurrentLayerIndex < _layers.Length)
        {
            LayerAdvanced?.Invoke();
        }

        Debug.Log($"Слой {CurrentLayerIndex - 1} очищен. Переход к слою {CurrentLayerIndex}");
    }
}