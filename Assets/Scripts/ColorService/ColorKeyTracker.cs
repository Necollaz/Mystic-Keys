using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Player.InventorySystem;
using Spawners.SpawnerKey;

namespace ColorService
{
    public class ColorKeyTracker
    {
        private readonly SpawnerKeys _keysSpawner;
        private readonly KeyInventory _keyInventory;
        
        private Dictionary<BaseColors, int> _availableKeys;
        
        public ColorKeyTracker(SpawnerKeys keysSpawner, KeyInventory keyInventory)
        {
            _keysSpawner = keysSpawner;
            _keyInventory = keyInventory;
            
            UpdateCounts();
        }
        
        public bool TrySelectColor(BaseColors color, int requiredKeys)
        {
            if (_availableKeys.TryGetValue(color, out int currentAvailable) && currentAvailable >= requiredKeys)
            {
                _availableKeys[color] -= requiredKeys;
                
                return true;
            }
            
            return false;
        }
        
        public bool HasKeys()
        {
            return _availableKeys.Any(kvp => kvp.Value > 0);
        }
        
        public int GetTotalForColor(BaseColors color)
        {
            _availableKeys.TryGetValue(color, out int count);
            
            return count;
        }
        
        private void UpdateCounts()
        {
            Dictionary<BaseColors, int> totalKeys = _keysSpawner.GetActive();
            Dictionary<BaseColors, int> inventoryKeys = _keyInventory.GetCounts();
            
            _availableKeys = new Dictionary<BaseColors, int>();
            
            HashSet<BaseColors> allColors = new HashSet<BaseColors>(totalKeys.Keys);
            
            allColors.UnionWith(inventoryKeys.Keys);
            
            foreach (BaseColors color in allColors)
            {
                totalKeys.TryGetValue(color, out int totalKeyCount);
                inventoryKeys.TryGetValue(color, out int inventoryKeyCount);
                
                int totalAvailable = totalKeyCount + inventoryKeyCount;
                _availableKeys[color] = Mathf.Max(totalAvailable, 0);
            }
        }
    }
}