using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnerLockbox : BaseSpawner<Lockbox>
{
    [SerializeField] private LockboxRegistry _lockboxRegistry;
    [SerializeField] private LockboxSpawnPoints _points;
    [SerializeField] private InactiveMarkerSpawner _inactiveMarkerSpawner;

    private SignalBus _signalBus;

    private SpawnerKeys _keysSpawner;
    private KeyInventory _keyInventory;
    private KeyLayer _keyLayer;

    private LockboxSpawnPointsAvailability _spawnPointAvailability;
    private LockboxCalculator _lockboxCalculator;
    private LockboxColorPicker _lockboxColorPicker;
    private ColorKeyTracker _colorKeyTracker;

    private HashSet<BaseColor> _processedColors = new HashSet<BaseColor>();

    [Inject]
    public void Construct(SignalBus signalBus, KeyLayer keyLayer)
    {
        _signalBus = signalBus;
        _keyLayer = keyLayer;

        _signalBus.Subscribe<SpawnerKeysCreatedSignal>(OnKeyCreated);
    }

    public override void Awake()
    {
        base.Awake();

        _lockboxCalculator = new LockboxCalculator();
        _lockboxColorPicker = new LockboxColorPicker();
        _spawnPointAvailability = new LockboxSpawnPointsAvailability(SpawnPoints, _points.InitialActivePoints);
    }

    private void OnEnable()
    {
        _lockboxRegistry.LockboxFilled += OnFilled;
    }

    private void OnDisable()
    {
        _lockboxRegistry.LockboxFilled -= OnFilled;
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<SpawnerKeysCreatedSignal>(OnKeyCreated);
    }

    public override void Create()
    {
        CreateInitial();
    }

    public void Initialize(KeyInventory keyInventory)
    {
        _keyInventory = keyInventory;
    }

    private void CreateInitial()
    {
        List<Transform> activeSpawnPoints = _spawnPointAvailability.GetActive();
        List<Transform> inactiveSpawnPoints = _spawnPointAvailability.GetInactive();

        _colorKeyTracker.UpdateKeyCounts();

        if (_colorKeyTracker.HasKeys())
        {
            int currentLayerIndex = _keyLayer.CurrentLayerIndex;
            List<BaseColor> lockboxColors = _lockboxColorPicker.GetColors(_keyLayer, currentLayerIndex, _colorKeyTracker, _processedColors);
            int lockboxIndex = 0;

            lockboxIndex = TryCreate(lockboxColors, activeSpawnPoints, lockboxIndex, currentLayerIndex);

            if (lockboxIndex < activeSpawnPoints.Count)
            {
                for (int layer = currentLayerIndex + 1; layer <= _keyLayer.Layers.Length; layer++)
                {
                    if (lockboxIndex >= activeSpawnPoints.Count)
                    {
                        break;
                    }

                    List<BaseColor> additionalColors = _lockboxColorPicker.GetColors(_keyLayer, layer, _colorKeyTracker, _processedColors);
                    lockboxIndex = TryCreate(additionalColors, activeSpawnPoints, lockboxIndex, layer);

                    if (lockboxIndex >= activeSpawnPoints.Count)
                    {
                        break;
                    }
                }
            }
        }

        foreach (Transform spawnPoint in inactiveSpawnPoints)
        {
            _inactiveMarkerSpawner.CreateInactiveMarker(spawnPoint);
        }
    }

    private int TryCreate(IEnumerable<BaseColor> colors, List<Transform> spawnPoints, int startIndex, int layerIndex)
    {
        foreach (BaseColor color in colors)
        {
            if (_colorKeyTracker.Reserve(color, _lockboxCalculator.KeysPerLockbox))
            {
                if (startIndex < spawnPoints.Count)
                {
                    CreateObject(spawnPoints[startIndex], color);
                    _processedColors.Add(color);
                    startIndex++;
                }
                else
                {
                    break;
                }
            }
        }

        return startIndex;
    }

    private void CreateObject(Transform spawnPoint, BaseColor color)
    {
        Lockbox lockbox = Pool.Get();
        lockbox.Initialize(color);
        _lockboxRegistry.Register(lockbox);
        SetTransform(lockbox, spawnPoint);
    }

    private void OnKeyCreated(SpawnerKeysCreatedSignal signal)
    {
        _keysSpawner = signal.SpawnerKeys;
        _colorKeyTracker = new ColorKeyTracker(_keysSpawner, _keyInventory);

        _colorKeyTracker.UpdateKeyCounts();
    }

    private void OnFilled(Lockbox filledLockbox)
    {
        HandleFilled(filledLockbox);
    }

    private void HandleFilled(Lockbox filledLockbox)
    {
        _lockboxRegistry.Unregister(filledLockbox);
        Pool.Return(filledLockbox);
        _colorKeyTracker.UpdateKeyCounts();

        Transform spawnPoint = filledLockbox.transform;

        if (_colorKeyTracker.HasKeys())
        {
            CreateNext(spawnPoint);
        }
    }

    private void CreateNext(Transform spawnPoint)
    {
        _colorKeyTracker.UpdateKeyCounts();

        if (_colorKeyTracker.HasKeys())
        {
            BaseColor? newColor = _lockboxColorPicker.GetNextAvailableColor(_keyLayer, _colorKeyTracker, _processedColors);

            if (newColor.HasValue)
            {
                if (_colorKeyTracker.Reserve(newColor.Value, _lockboxCalculator.KeysPerLockbox))
                {
                    CreateObject(spawnPoint, newColor.Value);
                }
            }
        }
    }
}