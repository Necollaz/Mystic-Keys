using System;
using System.Collections.Generic;

namespace YG
{
    [Serializable]
    public class SavesYG
    {
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public List<int> PurchasedInventorySlots = new List<int>();
        public List<int> PurchasedLockboxSlots = new List<int>();

        public SavesYG() { }
    }
}
