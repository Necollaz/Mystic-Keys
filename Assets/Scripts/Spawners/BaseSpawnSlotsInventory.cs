using System.Collections.Generic;
using UnityEngine;

public class BaseSpawnSlotsInventory
{
    [SerializeField] private Slot[] _slots;

    public void Purchase(int index)
    {
        if (index >= 0 && index < _slots.Length)
        {
            _slots[index].IsActive = true;
        }
    }

    public List<Slot> GetActive()
    {
        return new List<Slot>(_slots).FindAll(slot => slot.IsActive);
    }

    public List<Slot> GetInactive()
    {
        return new List<Slot>(_slots).FindAll(slot => slot.IsActive == false);
    }
}