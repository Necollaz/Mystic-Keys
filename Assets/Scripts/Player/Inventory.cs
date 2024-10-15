using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySpawnSlots _spawnSlots;
    [SerializeField] private LockboxRegistry _lockboxRegistry;
    [SerializeField] private ParticleSystem _moveToSlotEffect;
    [SerializeField] private ParticleSystem _moveToLockboxEffect;

    private List<Slot> _activeSlots;
    private List<Slot> _inactiveSlots;
    private Dictionary<Slot, Key> _activeKeys = new Dictionary<Slot, Key>();

    private void Awake()
    {
        UpdateSlotLists();
    }

    private void OnEnable()
    {
        _lockboxRegistry.LockboxCreated += OnMoveToLockbox;
    }

    private void OnDisable()
    {
        _lockboxRegistry.LockboxCreated -= OnMoveToLockbox;
    }
    
    public bool AddKey(Key key)
    {
        foreach (Slot slot in _activeSlots)
        {
            if (_activeKeys.ContainsKey(slot) == false)
            {
                _activeKeys[slot] = key;

                if(slot.SlotImage != null)
                {
                    slot.SlotImage.sprite = key.Get();
                    //slot.SlotImage.color = key.Color;
                }

                PlayEffect(slot);
                key.gameObject.SetActive(false);

                return true;
            }
        }

        return false;
    }
     
    public void PurchaseSlot(int index)
    {
        _spawnSlots.Purchase(index);
        UpdateSlotLists();
    }

    public bool HasSpace()
    {
        foreach (Slot slot in _activeSlots)
        {
            if (!_activeKeys.ContainsKey(slot))
            {
                return true;
            }
        }
        return false;
    }

    private void PlayEffect(Slot slot)
    {
        ParticleSystem effectInstance = Instantiate(_moveToLockboxEffect, slot.Transform.position, Quaternion.identity);
        effectInstance.Play();
    }

    private void RemoveKey(Slot slot)
    {
        if (_activeKeys.ContainsKey(slot))
        {
            Key key = _activeKeys[slot];
            _activeKeys.Remove(slot);

            slot.SlotImage.sprite = slot.DefaultSprite;
            slot.SlotImage.color = slot.DefaultColor;

            PlayEffect(slot);

            key.gameObject.SetActive(false);
        }
    }

    private void OnMoveToLockbox(Lockbox lockbox)
    {
        var keysToMove = _activeKeys
        .Where(pair => pair.Value.Color == lockbox.Color)
        .ToList();

        foreach (var pair in keysToMove)
        {
            RemoveKey(pair.Key);
            lockbox.AddKey();
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

            slot.SlotImage.sprite = slot.DefaultSprite;
            slot.SlotImage.color = slot.DefaultColor;
        }
    }
}