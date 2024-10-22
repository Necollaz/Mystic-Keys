using UnityEngine;

[RequireComponent(typeof(MovemingKeys))]
public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySpawnSlots _spawnSlots;
    [SerializeField] private LockboxRegistry _lockboxRegistry;

    private SlotOrganizer _slotOrganizer;
    private KeyInventory _keyInventory;
    private MovemingKeys _movemingKeys;

    private void Awake()
    {
        _slotOrganizer = new SlotOrganizer(_spawnSlots);
        _keyInventory = new KeyInventory();

        _movemingKeys = GetComponent<MovemingKeys>();
        _movemingKeys.Initialize(_keyInventory);
    }

    private void OnEnable()
    {
        _lockboxRegistry.LockboxCreated += HandleLockboxCreated;
    }

    private void OnDisable()
    {
        _lockboxRegistry.LockboxCreated -= HandleLockboxCreated;
    }

    public bool AddKey(Key key)
    {
        return _keyInventory.Add(_slotOrganizer.GetActive(), key);
    }

    public void PurchaseSlot(int index)
    {
        _slotOrganizer.Purchase(index);
    }

    public bool HasSpace()
    {
        return _keyInventory.HasSpace(_slotOrganizer.GetActive());
    }

    private void HandleLockboxCreated(Lockbox lockbox)
    {
        lockbox.Opened += _movemingKeys.LockboxOpened;
    }
}