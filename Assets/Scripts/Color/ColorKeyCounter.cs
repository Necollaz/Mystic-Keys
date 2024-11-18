using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorKeyCounter
{
    private SpawnerKeys _keysSpawner;
    private KeyInventory _keyInventory;

    private Dictionary<BaseColor, int> _availableKeys;
    private Dictionary<BaseColor, int> _reservedKeys = new Dictionary<BaseColor, int>();

    public ColorKeyCounter(SpawnerKeys keysSpawner, KeyInventory keyInventory)
    {
        _keysSpawner = keysSpawner;
        _keyInventory = keyInventory;
        UpdateKeyCounts();
    }

    public void UpdateKeyCounts()
    {
        if (_keysSpawner == null || _keyInventory == null)
        {
            Debug.LogError("KeysSpawner или KeyInventory не инициализирован");
            return;
        }

        Dictionary<BaseColor, int> totalKeys = _keysSpawner.GetActive();
        Dictionary<BaseColor, int> inventoryKeys = _keyInventory.GetKeyCounts();
        _availableKeys = new Dictionary<BaseColor, int>();

        foreach (var kvp in totalKeys)
        {
            int reservedCount = _reservedKeys.ContainsKey(kvp.Key) ? _reservedKeys[kvp.Key] : 0;
            int inventoryCount = inventoryKeys.ContainsKey(kvp.Key) ? inventoryKeys[kvp.Key] : 0;

            _availableKeys[kvp.Key] = kvp.Value + inventoryCount - reservedCount;
        }
    }

    public bool Reserve(BaseColor color, int count)
    {
        if (_availableKeys.ContainsKey(color) && _availableKeys[color] >= count)
        {
            _availableKeys[color] -= count;

            if (_reservedKeys.ContainsKey(color))
            {
                _reservedKeys[color] += count;
            }
            else
            {
                _reservedKeys[color] = count;
            }

            return true;
        }

        return false;
    }

    public Dictionary<BaseColor, int> GetPerColor()
    {
        return new Dictionary<BaseColor, int>(_availableKeys);
    }

    public bool HasKeys()
    {
        return _availableKeys.Values.Any(count => count > 0);
    }
}