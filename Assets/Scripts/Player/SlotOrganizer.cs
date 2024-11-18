using System.Collections.Generic;

public class SlotOrganizer
{
    private InventorySpawnSlots _spawnSlots;
    private List<Slot> _activeSlots;
    private List<Slot> _inactiveSlots;

    public SlotOrganizer(InventorySpawnSlots spawnSlots)
    {
        _spawnSlots = spawnSlots;
        UpdateLists();
    }

    public void UpdateLists()
    {
        _activeSlots = _spawnSlots.GetActive();
        _inactiveSlots = _spawnSlots.GetInactive();

        foreach (Slot slot in _inactiveSlots)
        {
            slot.DeactivateSlot();
        }
    }

    public void Purchase(int index)
    {
        _spawnSlots.Purchase(index);
        UpdateLists();
    }

    public List<Slot> GetActive()
    {
        return _activeSlots;
    }
}