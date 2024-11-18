using UnityEngine;
using Zenject;

[RequireComponent(typeof(MovemingKeys))]
public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySpawnSlots _spawnSlots;

    private LockboxRegistry _lockboxRegistry;
    private SpawnerLockbox _spawnerLockbox;

    private SlotOrganizer _slotOrganizer;
    private KeyInventory _keyInventory;
    private MovemingKeys _movemingKeys;

    [Inject]
    public void Construct(LockboxRegistry lockboxRegistry, SpawnerLockbox spawnerLockbox)
    {
        _lockboxRegistry = lockboxRegistry;
        _spawnerLockbox = spawnerLockbox;
    }

    private void Awake()
    {
        _slotOrganizer = new SlotOrganizer(_spawnSlots);
        _keyInventory = new KeyInventory();

        _movemingKeys = GetComponent<MovemingKeys>();
        _movemingKeys.Initialize(_keyInventory);
        _spawnerLockbox.Initialize(_keyInventory);
    }

    private void OnEnable()
    {
        _lockboxRegistry.LockboxCreated += OnLockboxCreated;
    }

    private void OnDisable()
    {
        _lockboxRegistry.LockboxCreated -= OnLockboxCreated;
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

    private void OnLockboxCreated(Lockbox lockbox)
    {
        lockbox.Opened += _movemingKeys.LockboxOpened;
    }
}