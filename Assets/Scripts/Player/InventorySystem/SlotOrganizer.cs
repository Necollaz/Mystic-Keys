using System.Collections.Generic;
using Spawners.SpawnerInventorySlot;

namespace Player.InventorySystem
{
    public class SlotOrganizer
    {
        private InventorySpawnSlots _spawnSlots;
        private List<Slot> _activeSlots;
        private List<Slot> _inactiveSlots;
        private SlotData _slotData;

        public int MaxActiveSlots { get; private set; } = 4;

        public SlotOrganizer(InventorySpawnSlots spawnSlots, SlotData slotData)
        {
            _spawnSlots = spawnSlots;
            _slotData = slotData;
            Initialize();
        }

        public List<Slot> GetActive()
        {
            return _activeSlots;
        }

        public void ActivateNext()
        {
            if (_activeSlots.Count >= MaxActiveSlots)
            {
                return;
            }

            Slot[] allSlots = _spawnSlots.GetAll();

            for (int i = 0; i < allSlots.Length; i++)
            {
                Slot currentSlot = allSlots[i];

                if (!currentSlot.IsActive)
                {
                    currentSlot.ActivateSlot();
                    _slotData.PurchasedInventorySlots.Add(i);

                    UpdateLists();

                    return;
                }
            }
        }

        private void Initialize()
        {
            Slot[] allSlots = _spawnSlots.GetAll();

            foreach (int slotIndex in _slotData.PurchasedInventorySlots)
            {
                if (slotIndex >= 0 && slotIndex < allSlots.Length)
                {
                    Slot slotToActivate = allSlots[slotIndex];
                    slotToActivate.ActivateSlot();
                }
            }

            UpdateLists();
        }

        private void UpdateLists()
        {
            _activeSlots = _spawnSlots.GetActive();
            _inactiveSlots = _spawnSlots.GetInactive();

            foreach (Slot slot in _inactiveSlots)
            {
                slot.DeactivateSlot();
            }
        }
    }
}