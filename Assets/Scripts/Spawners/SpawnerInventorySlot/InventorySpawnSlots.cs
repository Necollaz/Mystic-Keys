using System;
using Player.InventorySystem;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners.SpawnerInventorySlot
{
    [Serializable]
    public class InventorySpawnSlots
    {
        [SerializeField] private List<Slot> _slots;

        public List<Slot> GetAll()
        {
            return _slots;
        }

        public List<Slot> GetActive()
        {
            return new List<Slot>(_slots).FindAll(slot => slot.IsActive);
        }

        public List<Slot> GetInactive()
        {
            return new List<Slot>(_slots).FindAll(slot => !slot.IsActive);
        }
    }
}