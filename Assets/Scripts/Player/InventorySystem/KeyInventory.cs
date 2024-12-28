using System.Collections.Generic;
using System.Linq;
using BaseElements.FolderKey;
using ColorService;
using UnityEngine;

namespace Player.InventorySystem
{
    public class KeyInventory
    {
        private Dictionary<Slot, Key> _activeKeys = new Dictionary<Slot, Key>();

        public bool Add(List<Slot> activeSlots, Key key)
        {
            foreach (Slot slot in activeSlots)
            {
                if (_activeKeys.ContainsKey(slot) == false)
                {
                    _activeKeys[slot] = key;
                    slot.SetKeySprite(key.GetSprite());

                    return true;
                }
            }

            return false;
        }

        public void Remove(Slot slot)
        {
            if (_activeKeys.ContainsKey(slot))
            {
                _activeKeys.Remove(slot);
                slot.ResetSprite();
            }
        }

        public bool HasSpace(List<Slot> activeSlots)
        {
            return activeSlots.Any(slot => !_activeKeys.ContainsKey(slot));
        }

        public List<KeyValuePair<Slot, Key>> GetByColor(BaseColors color)
        {
            return _activeKeys
                .Where(key => key.Value.Color == color)
                .ToList();
        }

        public Dictionary<BaseColors, int> GetCounts()
        {
            return _activeKeys.Values
                .GroupBy(key => key.Color)
                .ToDictionary(group => group.Key, group => group.Count());
        }

        public BaseColors? GetRandomColor()
        {
            BaseColors[] colors = _activeKeys.Select(kvp => kvp.Value.Color).ToArray();

            if (colors.Length == 0)
            {
                return null;
            }

            return colors[Random.Range(0, colors.Length)];
        }
    }
}