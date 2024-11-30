using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorKeyTracker
{
    private readonly SpawnerKeys _keysSpawner;
    private readonly KeyInventory _keyInventory;

    private Dictionary<BaseColor, int> _availableKeys;
    private Dictionary<BaseColor, int> _reservedKeys;

    public ColorKeyTracker(SpawnerKeys keysSpawner, KeyInventory keyInventory)
    {
        _keysSpawner = keysSpawner;
        _keyInventory = keyInventory;
        _reservedKeys = new Dictionary<BaseColor, int>();

        UpdateKeyCounts();
    }

    public void UpdateKeyCounts()
    {
        Dictionary<BaseColor, int> totalKeys = _keysSpawner.GetActive();
        Dictionary<BaseColor, int> inventoryKeys = _keyInventory.GetKeyCounts();

        _availableKeys = new Dictionary<BaseColor, int>();

        HashSet<BaseColor> allColors = new HashSet<BaseColor>(totalKeys.Keys);
        allColors.UnionWith(inventoryKeys.Keys);

        foreach (BaseColor color in allColors)
        {
            totalKeys.TryGetValue(color, out int totalKeyCount);
            inventoryKeys.TryGetValue(color, out int inventoryKeyCount);
            _reservedKeys.TryGetValue(color, out int reservedKeyCount);

            int totalAvailable = totalKeyCount + inventoryKeyCount - reservedKeyCount;
            _availableKeys[color] = Mathf.Max(totalAvailable, 0);
        }
    }

    public bool Reserve(BaseColor color, int count)
    {
        if (_availableKeys.TryGetValue(color, out int available) && available >= count)
        {
            _reservedKeys[color] = _reservedKeys.ContainsKey(color) ? _reservedKeys[color] + count : count;
            _availableKeys[color] -= count;

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
        return _availableKeys.Any(kvp => kvp.Value > 0);
    }

    public int GetTotalKeysForColor(BaseColor color)
    {
        _availableKeys.TryGetValue(color, out int count);
        return count;
    }
}

//private SpawnerKeys _keysSpawner;
//private KeyInventory _keyInventory;

//private Dictionary<BaseColor, int> _availableKeys;
//private Dictionary<BaseColor, int> _reservedKeys = new Dictionary<BaseColor, int>();

//public ColorKeyTracker(SpawnerKeys keysSpawner, KeyInventory keyInventory)
//{
//    _keysSpawner = keysSpawner;
//    _keyInventory = keyInventory;

//    UpdateKeyCounts();
//}

//public void UpdateKeyCounts()
//{
//    if (_keysSpawner == null || _keyInventory == null)
//    {
//        Debug.LogError("KeysSpawner или KeyInventory не инициализирован");
//        return;
//    }

//    Dictionary<BaseColor, int> totalKeys = _keysSpawner.GetActive();
//    Dictionary<BaseColor, int> inventoryKeys = _keyInventory.GetKeyCounts();

//    HashSet<BaseColor> allColors = new HashSet<BaseColor>(totalKeys.Keys);

//    foreach (BaseColor color in inventoryKeys.Keys)
//    {
//        allColors.Add(color);
//    }

//    _availableKeys = new Dictionary<BaseColor, int>();

//    foreach (BaseColor color in allColors)
//    {
//        int total = totalKeys.ContainsKey(color) ? totalKeys[color] : 0;
//        int inventory = inventoryKeys.ContainsKey(color) ? inventoryKeys[color] : 0;
//        int reserved = _reservedKeys.ContainsKey(color) ? _reservedKeys[color] : 0;

//        int available = total + inventory - reserved;

//        _availableKeys[color] = available;
//    }
//}

//public bool Reserve(BaseColor color, int count)
//{
//    if (_availableKeys.ContainsKey(color) && _availableKeys[color] >= count)
//    {
//        _availableKeys[color] -= count;

//        if (_reservedKeys.ContainsKey(color))
//        {
//            _reservedKeys[color] += count;
//        }
//        else
//        {
//            _reservedKeys[color] = count;
//        }

//        return true;
//    }

//    return false;
//}

//public bool Unreserve(BaseColor color, int count)
//{
//    if (_reservedKeys.ContainsKey(color) && _reservedKeys[color] >= count)
//    {
//        _reservedKeys[color] -= count;
//        _availableKeys[color] += count;

//        return true;
//    }

//    return false;
//}

//public Dictionary<BaseColor, int> GetPerColor()
//{
//    return new Dictionary<BaseColor, int>(_availableKeys);
//}

//public bool HasKeys(int keysPerLockbox)
//{
//    return _availableKeys.Values.Any(count => count > keysPerLockbox);
//}