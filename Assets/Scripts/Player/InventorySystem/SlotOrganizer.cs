using System.Collections.Generic;
using SavesDataSlot;
using Spawners.SpawnerInventorySlot;

namespace Player.InventorySystem
{
    public class SlotOrganizer
    {
        private readonly InventorySpawnSlots _spawnSlots;
        private readonly SavesData _savesData;
        private readonly SlotDataStorage _slotDataManager;
        
        public SlotOrganizer(InventorySpawnSlots spawnSlots, SavesData savesData, SlotDataStorage slotDataManager)
        {
            _spawnSlots = spawnSlots;
            _savesData = savesData;
            _slotDataManager = slotDataManager;
            Initialize();
        }
        
        public int MaxActiveSlots { get; private set; } = 4;
        
        public List<Slot> GetActive()
        {
            return _spawnSlots.GetActive();
        }
        
        public void ActivateNext()
        {
            if (_spawnSlots.GetActive().Count >= MaxActiveSlots)
            {
                return;
            }
            
            List<Slot> allSlots = _spawnSlots.GetAll();
            
            for (int i = 0; i < allSlots.Count; i++)
            {
                Slot currentSlot = allSlots[i];
                
                if (!currentSlot.IsActive)
                {
                    currentSlot.Activate();
                    
                    if (!_savesData.PurchasedInventorySlots.Contains(i))
                    {
                        _spawnSlots.GetActive().Add(currentSlot);
                        _savesData.PurchasedInventorySlots.Add(i);
                        _slotDataManager.Save();
                    }
                    
                    UpdateLists();
                    
                    return;
                }
            }
        }
        
        private void Initialize()
        {
            List<Slot> allSlots = _spawnSlots.GetAll();
            
            foreach (int slotIndex in _savesData.PurchasedInventorySlots)
            {
                if (slotIndex >= 0 && slotIndex < allSlots.Count)
                {
                    Slot slotToActivate = allSlots[slotIndex];
                    
                    if (!slotToActivate.IsActive)
                    {
                        slotToActivate.Activate();
                    }
                }
            }
            
            UpdateLists();
        }
        
        private void UpdateLists()
        {
            List<Slot> inactiveSlots = _spawnSlots.GetInactive();
            
            foreach (Slot slot in inactiveSlots)
            {
                slot.Deactivate();
            }
        }
    }
}