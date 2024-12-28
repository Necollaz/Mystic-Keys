using System;
using UnityEngine;
using System.Collections.Generic;
using Player.InventorySystem;

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