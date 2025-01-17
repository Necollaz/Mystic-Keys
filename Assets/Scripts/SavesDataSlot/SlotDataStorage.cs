using System.Collections.Generic;
using UnityEngine;
using YG;

namespace SavesDataSlot
{
    public class SlotDataStorage : MonoBehaviour
    {
        [SerializeField] private SavesData _savesData;
        
        public SavesData SavesDataKey => _savesData;
        
        private void OnEnable()
        {
            YandexGame.GetDataEvent += OnLoad;
        }
        
        private void OnDisable()
        {
            YandexGame.GetDataEvent -= OnLoad;
        }
        
        private void Awake()
        {
            if (YandexGame.SDKEnabled)
            {
                OnLoad();
            }
            else
            {
                InitializeDefault();
            }
        }
        
        public void Save()
        {
            if (YandexGame.savesData == null)
            {
                YandexGame.savesData = new SavesYG();
            }
            
            YandexGame.savesData.PurchasedInventorySlots = new List<int>(_savesData.PurchasedInventorySlots);
            YandexGame.savesData.PurchasedLockboxSlots = new List<int>(_savesData.PurchasedLockboxSlots);
            YandexGame.SaveProgress();
        }
        
        private void OnLoad()
        {
            if (YandexGame.savesData != null)
            {
                _savesData.PurchasedInventorySlots = new List<int>(YandexGame.savesData.PurchasedInventorySlots);
                _savesData.PurchasedLockboxSlots = new List<int>(YandexGame.savesData.PurchasedLockboxSlots);
            }
            else
            {
                InitializeDefault();
                Save();
            }
        }
        
        private void InitializeDefault()
        {
            InitializeList(ref _savesData.PurchasedLockboxSlots);
            InitializeList(ref _savesData.PurchasedInventorySlots);
        }
        
        public void Reset()
        {
            ClearAndInitializeList(ref _savesData.PurchasedLockboxSlots);
            ClearAndInitializeList(ref _savesData.PurchasedInventorySlots);
            Save();
        }
        
        private void InitializeList(ref List<int> slotList)
        {
            if (slotList == null || slotList.Count == 0)
            {
                slotList = new List<int> { 0 };
                Save();
            }
        }
        
        private void ClearAndInitializeList(ref List<int> slotList)
        {
            slotList.Clear();
            InitializeList(ref slotList);
        }
    }
}