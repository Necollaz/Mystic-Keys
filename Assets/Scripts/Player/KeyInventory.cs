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

                PlayEffect(slot);
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

            PlayEffect(slot);
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

    private void PlayEffect(Slot slot)
    {
        GameObject.Instantiate(slot.KeyRemovedEffect, slot.Transform.position, Quaternion.identity, slot.Transform);
    }
}