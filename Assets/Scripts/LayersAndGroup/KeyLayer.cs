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

    public void Register(int layerIndex, Key key)
    {
        if(_activeKeysPerLayer.ContainsKey(layerIndex) == false)
        {
            _activeKeysPerLayer[layerIndex] = new HashSet<Key>();
        }

        _activeKeysPerLayer[layerIndex].Add(key);
        key.SetInteractivity(IsCurrent(layerIndex));
        //Debug.Log($"Ключ зарегистрирован в слое {layerIndex}. Интерактивность: {IsCurrent(layerIndex)}");
    }

    public void Unregister(int layerIndex, Key key)
    {
        if (_activeKeysPerLayer.ContainsKey(layerIndex))
        {
            _activeKeysPerLayer[layerIndex].Remove(key);
            //Debug.Log($"Ключ удалён из слоя {layerIndex}. Осталось ключей: {_activeKeysPerLayer[layerIndex].Count}");
            if (_activeKeysPerLayer[layerIndex].Count == 0)
            {
                _activeKeysPerLayer.Remove(layerIndex);
                CurrentLayerIndex++;
                //Debug.Log($"Переключение на слой {CurrentLayerIndex}");
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
                    //Debug.Log($"Интерактивность ключа в слое {CurrentLayerIndex} установлена в true");
                }
            }
            else
            {
                //Debug.LogWarning($"Нет ключей для слоя с индексом {CurrentLayerIndex}. Возможно, слой не был правильно зарегистрирован.");
            }
        }
        else
        {
            //Debug.Log("Все слои пройдены. Игрок собрал все ключи.");
        }
    }
}