using System.Collections.Generic;
using System.Linq;

public class ColorKeyCounter
{
    private readonly SpawnerKeys _keysSpawner;

    public ColorKeyCounter(SpawnerKeys keysSpawner)
    {
        _keysSpawner = keysSpawner;
    }

    public Dictionary<BaseColor, int> CountKeysPerColor()
    {
        Dictionary<BaseColor, int> colorKeyCounts = new Dictionary<BaseColor, int>();
        List<Key> activeKeys = _keysSpawner.GetActiveKeys();

        foreach (var key in activeKeys)
        {
            BaseColor color = key.Color;

            if (colorKeyCounts.ContainsKey(color))
            {
                colorKeyCounts[color]++;
            }
            else
            {
                colorKeyCounts[color] = 1;
            }
        }

        return colorKeyCounts;
    }

    public bool HasKeys()
    {
        Dictionary<BaseColor, int> colorKeyCounts = CountKeysPerColor();

        return colorKeyCounts.Values.Any(count => count > 0);
    }
}