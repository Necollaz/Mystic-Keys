using UnityEngine;

[RequireComponent(typeof(LockboxInteractor))]
public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySpawnSlots _spawnSlots;
    [SerializeField] private LockboxRegistry _lockboxRegistry;
    [SerializeField] private ParticlePool _particlePool;

    private SlotOrganizer _slotOrganizer;
    private KeyInventory _keyInventory;
    private Effects _effects;
    private LockboxInteractor _lockboxInteractor;

    private void Awake()
    {
        _effects = new Effects(_particlePool, this);
        _slotOrganizer = new SlotOrganizer(_spawnSlots);
        _keyInventory = new KeyInventory(_effects);

        _lockboxInteractor = GetComponent<LockboxInteractor>();
        _lockboxInteractor.Initialize(_keyInventory, _effects);
    }

    private void OnEnable()
    {
        _lockboxRegistry.LockboxCreated += _lockboxInteractor.Move;
    }

    private void OnDisable()
    {
        _lockboxRegistry.LockboxCreated -= _lockboxInteractor.Move;
    }

    public bool AddKey(Key key)
    {
        return _keyInventory.Add(_slotOrganizer.GetActiveSlots(), key);
    }

    public void PurchaseSlot(int index)
    {
        _slotOrganizer.PurchaseSlot(index);
    }

    public bool HasSpace()
    {
        return _keyInventory.HasSpace(_slotOrganizer.GetActiveSlots());
    }
}