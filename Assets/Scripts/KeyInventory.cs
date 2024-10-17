using System.Collections.Generic;
using System.Linq;

public class KeyInventory
{
    private Dictionary<Slot, Key> _activeKeys = new Dictionary<Slot, Key>();
    private Effects _effects;

    public KeyInventory(Effects effects)
    {
        _effects = effects;
    }

    public bool Add(List<Slot> activeSlots, Key key)
    {
        foreach (Slot slot in activeSlots)
        {
            if (_activeKeys.ContainsKey(slot) == false)
            {
                _activeKeys[slot] = key;

                if (slot.SlotImage != null)
                {
                    slot.SlotImage.sprite = key.GetSprite();
                }

                _effects.Play(slot);
                key.gameObject.SetActive(false);
                return true;
            }
        }
        return false;
    }

    public void Remove(Slot slot)
    {
        if (_activeKeys.ContainsKey(slot))
        {
            Key key = _activeKeys[slot];
            _activeKeys.Remove(slot);

            if (slot.SlotImage != null)
            {
                slot.SlotImage.sprite = slot.DefaultSprite;
                slot.SlotImage.color = slot.DefaultColor;
            }

            _effects.Play(slot);
            key.gameObject.SetActive(true);
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
            .Where(pair => pair.Value.Color == color)
            .ToList();
    }
}