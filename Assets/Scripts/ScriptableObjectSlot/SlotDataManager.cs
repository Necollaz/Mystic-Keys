using UnityEngine;

namespace ScriptableObjectSlot
{
    public class SlotDataManager : MonoBehaviour
    {
        private const string SlotData = "SlotData";

        [SerializeField] private SlotData _slotData;

        public SlotData Slot => _slotData;

        private void Awake()
        {
            Load();
            InitializeDefault();
        }

        private void InitializeDefault()
        {
            if (_slotData.PurchasedLockboxSlots == null || _slotData.PurchasedLockboxSlots.Count == 0)
            {
                _slotData.PurchasedLockboxSlots.Add(0);
                Save();
            }

            if(_slotData.PurchasedInventorySlots == null || _slotData.PurchasedInventorySlots.Count == 0)
            {
                _slotData.PurchasedInventorySlots.Add(0);
                Save();
            }
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(_slotData);

            PlayerPrefs.SetString(SlotData, json);
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(SlotData))
            {
                string json = PlayerPrefs.GetString(SlotData);

                JsonUtility.FromJsonOverwrite(json, _slotData);
            }
        }

        public void Reset()
        {
            _slotData.PurchasedLockboxSlots.Clear();
            _slotData.PurchasedInventorySlots.Clear();
            InitializeDefault();
            Save();
        }
    }
}