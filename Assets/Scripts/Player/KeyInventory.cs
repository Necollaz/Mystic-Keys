using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

                if (slot.SlotImage != null)
                {
                    slot.SlotImage.sprite = key.GetSprite();
                }

                GameObject.Instantiate(slot.KeyAddedEffect, slot.Transform.position, Quaternion.identity, slot.Transform);
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

            if (slot.SlotImage != null)
            {
                slot.SlotImage.sprite = slot.DefaultSprite;
                slot.SlotImage.color = slot.DefaultColor;
            }

            GameObject.Instantiate(slot.KeyRemovedEffect, slot.Transform.position, Quaternion.identity, slot.Transform);
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
}