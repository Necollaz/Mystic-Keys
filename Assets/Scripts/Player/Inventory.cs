using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySpawnSlots _spawnSlots;
    [SerializeField] private LockboxRegistry _lockboxRegistry;

    private List<Slot> _activeSlots;
    private List<Slot> _inactiveSlots;
    private Dictionary<Slot, Key> _activeKeys = new Dictionary<Slot, Key>();

    private void Awake()
    {
        InitializeSlots();
    }

    private void OnEnable()
    {
        _lockboxRegistry.LockboxCreated += OnHandledNewLockbox;
    }

    private void OnDisable()
    {
        _lockboxRegistry.LockboxCreated -= OnHandledNewLockbox;
    }

    private void InitializeSlots()
    {
        UpdateSlotLists();
    }

    public bool AddKey(Key key)
    {
        foreach (Slot slot in _activeSlots)
        {
            if (_activeKeys.ContainsKey(slot) == false)
            {
                _activeKeys[slot] = key;

                key.transform.SetParent(slot.Transform);
                key.transform.localPosition = Vector3.zero;
                key.transform.localRotation = Quaternion.identity;

                return true;
            }
        }

        return false;
    }

    public void RemoveKey(Slot slot)
    {
        if (_activeKeys.ContainsKey(slot))
        {
            Key key = _activeKeys[slot];
            key.transform.SetParent(null);
            key.gameObject.SetActive(false);
            _activeKeys.Remove(slot);
        }
    }

    public List<Slot> GetActiveSlots()
    {
        return _activeSlots;
    }

    public void PurchaseSlot(int index)
    {
        _spawnSlots.Purchase(index);
        UpdateSlotLists();
    }

    private void OnHandledNewLockbox(Lockbox lockbox)
    {
        foreach (var pair in _activeKeys)
        {
            Key key = pair.Value;

            if (key.Color == lockbox.Color)
            {
                RemoveKey(pair.Key);
                lockbox.AddKey();
                break;
            }
        }
    }

    private void UpdateSlotLists()
    {
        _activeSlots = _spawnSlots.GetActive();
        _inactiveSlots = _spawnSlots.GetInactive();

        foreach (Slot slot in _inactiveSlots)
        {
            if (slot.InactiveSlot != null)
            {
                Instantiate(slot.InactiveSlot, slot.Transform.position, Quaternion.identity, slot.Transform);
            }
        }
    }
}