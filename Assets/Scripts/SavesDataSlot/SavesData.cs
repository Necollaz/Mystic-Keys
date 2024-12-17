using System;
using System.Collections.Generic;

namespace SavesDataSlot
{
    [Serializable]
    public class SavesData
    {
        public List<int> PurchasedInventorySlots = new List<int>();
        public List<int> PurchasedLockboxSlots = new List<int>();
    }
}