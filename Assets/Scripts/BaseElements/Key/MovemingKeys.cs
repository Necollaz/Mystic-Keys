using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovemingKeys : MonoBehaviour
{
    private KeyInventory _keyInventory;

    public void Initialize(KeyInventory keyInventory)
    {
        _keyInventory = keyInventory;
    }

    public void LockboxOpened(Lockbox lockbox)
    {
        Move(lockbox);
    }

    private void Move(Lockbox lockbox)
    {
        int keysNeeded = lockbox.RequiredKeys - lockbox.CurrentKeyCount;

        List<KeyValuePair<Slot, Key>> keysToMove = _keyInventory.GetByColor(lockbox.Color)
            .Take(keysNeeded)
            .ToList();

        foreach (KeyValuePair<Slot, Key> pair in keysToMove)
        {
            Slot slot = pair.Key;

            _keyInventory.Remove(slot);
            lockbox.AddKey();
        }
    }
}