using System.Collections.Generic;
using System.Linq;

public class ColorKeyCounter
{
    private SpawnerKeys _keysSpawner;
    private Dictionary<BaseColor, int> _availableKeys;

    public ColorKeyCounter(SpawnerKeys keysSpawner)
    {
        _keysSpawner = keysSpawner;
        UpdateKeyCounts();
    }

    public void UpdateKeyCounts()
    {
        _availableKeys = _keysSpawner.GetActive();
    }

    public bool ReserveKeys(BaseColor color, int count)
    {
        if (_availableKeys.ContainsKey(color) && _availableKeys[color] >= count)
        {
            _availableKeys[color] -= count;
            return true;
        }
        return false;
    }

    public Dictionary<BaseColor, int> GetKeysPerColor()
    {
        return new Dictionary<BaseColor, int>(_availableKeys);
    }

    public bool HasKeys()
    {
        return _availableKeys.Values.Any(count => count > 0);
    }
}