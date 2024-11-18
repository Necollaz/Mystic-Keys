using System.Collections.Generic;
using System.Linq;

public class KeyInventory
{
    private Dictionary<Slot, Key> _activeKeys = new Dictionary<Slot, Key>();

    public bool Add(List<Slot> activeSlots, Key key)
    {
        foreach (Slot slot in activeSlots)
        {
            if (_activeKeys.ContainsKey(slot) == false)
            {
                _activeKeys[slot] = key;
                slot.SetKeySprite(key.GetSprite());
                return true;
            }
        }

        return false;
    }

    public void Remove(Slot slot)
    {
        if (_activeKeys.ContainsKey(slot))
        {
            _activeKeys.Remove(slot);
            slot.ResetSprite();
        }
    }

    public bool HasSpace(List<Slot> activeSlots)
    {
        foreach (Slot slot in activeSlots)
        {
            if (_activeKeys.ContainsKey(slot) == false)
            {
                return true;
            }
        }

        return false;
    }

    public List<KeyValuePair<Slot, Key>> GetByColor(BaseColor color)
    {
        return _activeKeys
            .Where(key => key.Value.Color == color)
            .ToList();
    }

    public Dictionary<BaseColor, int> GetKeyCounts()
    {
        Dictionary<BaseColor, int> colorCounts = new Dictionary<BaseColor, int>();

        foreach (var kvp in _activeKeys)
        {
            BaseColor color = kvp.Value.Color;

            if (!colorCounts.ContainsKey(color))
            {
                colorCounts[color] = 0;
            }

            colorCounts[color]++;
        }

        return colorCounts;
    }
}