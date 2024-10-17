using System.Collections.Generic;
using System.Linq;

public class ColorKeyCounter
{
    private SpawnerKeys _keysSpawner;
    private Dictionary<BaseColor, int> _availableKeys;
    private Dictionary<BaseColor, int> _reservedKeys = new Dictionary<BaseColor, int>();

    public ColorKeyCounter(SpawnerKeys keysSpawner)
    {
        _keysSpawner = keysSpawner;
        UpdateKeyCounts();
    }

    public void UpdateKeyCounts()
    {
        Dictionary<BaseColor, int> totalKeys = _keysSpawner.GetActive();
        _availableKeys = new Dictionary<BaseColor, int>();

        foreach (var kvp in totalKeys)
        {
            int reservedCount = _reservedKeys.ContainsKey(kvp.Key) ? _reservedKeys[kvp.Key] : 0;
            _availableKeys[kvp.Key] = kvp.Value - reservedCount;
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