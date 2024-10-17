using System.Collections.Generic;
using UnityEngine;

public class SlotOrganizer
{
    private InventorySpawnSlots _spawnSlots;
    private List<Slot> _activeSlots;
    private List<Slot> _inactiveSlots;

    public SlotOrganizer(InventorySpawnSlots spawnSlots)
    {
        _spawnSlots = spawnSlots;
        UpdateSlotLists();
    }

    public void UpdateSlotLists()
    {
        _activeSlots = _spawnSlots.GetActive();
        _inactiveSlots = _spawnSlots.GetInactive();

        foreach (Slot slot in _inactiveSlots)
        {
            if (slot.InactiveSlot != null)
            {
                GameObject.Instantiate(slot.InactiveSlot, slot.Transform.position, Quaternion.identity, slot.Transform);
            }
            if (slot.SlotImage != null)
            {
                slot.SlotImage.sprite = slot.DefaultSprite;
                slot.SlotImage.color = slot.DefaultColor;
            }
        }
    }

    public void PurchaseSlot(int index)
    {
        _spawnSlots.Purchase(index);
        UpdateSlotLists();
    }

    public List<Slot> GetActiveSlots()
    {
        return _activeSlots;
    }
}