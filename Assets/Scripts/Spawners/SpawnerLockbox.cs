using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLockbox : BaseSpawner<Lockbox>
{
    [SerializeField] private LockboxRegistry _lockboxRegistry;
    [SerializeField] private SpawnerKeys _keysSpawner;
    [SerializeField] private ParticleSystem _inactivePrefab;

    private LockboxSpawnPointsAvailability _spawnPointAvailability;
    private LockboxCalculator _lockboxCalculator;
    private LockboxColorPicker _lockboxColorPicker;
    private ColorKeyCounter _colorKeyCounter;

    public override void Awake()
    {
        base.Awake();
        _colorKeyCounter = new ColorKeyCounter(_keysSpawner);
        _lockboxCalculator = new LockboxCalculator();
        _lockboxColorPicker = new LockboxColorPicker();
        _spawnPointAvailability = new LockboxSpawnPointsAvailability(SpawnPoints);
    }

    private void OnEnable()
    {
        _lockboxRegistry.LockboxFilled += OnFilled;
    }

    private void OnDisable()
    {
        _lockboxRegistry.LockboxFilled -= OnFilled;
    }

    public override void Spawn()
    {
        CreateInitial();
    }

    private void CreateInitial()
    {
        _colorKeyCounter.UpdateKeyCounts();

        if (_colorKeyCounter.HasKeys() == false)
        {
            return;
        }

        List<Transform> activeSpawnPoints = _spawnPointAvailability.GetActive();
        List<Transform> inactiveSpawnPoints = _spawnPointAvailability.GetInactive();

        Dictionary<BaseColor, int> colorKeyCounts = _colorKeyCounter.GetPerColor();
        Dictionary<BaseColor, int> lockboxesPerColor = _lockboxCalculator.CalculatePerColor(colorKeyCounts);
        List<BaseColor> lockboxColors = _lockboxColorPicker.GetColors(lockboxesPerColor, activeSpawnPoints.Count);

        for (int i = 0; i < lockboxColors.Count; i++)
        {
            BaseColor color = lockboxColors[i];
            bool reserved = _colorKeyCounter.Reserve(color, _lockboxCalculator.KeysPerLockbox);

            if (reserved)
            {
                Transform spawnPoint = activeSpawnPoints[i];
                Create(spawnPoint, color);
            }
        }

        foreach (Transform spawnPoint in inactiveSpawnPoints)
        {
            CreateInactiveMarker(spawnPoint);
        }
    }

    private void OnFilled(Lockbox filledLockbox)
    {
        StartCoroutine(HandleFilled(filledLockbox));
    }

    private IEnumerator HandleFilled(Lockbox filledLockbox)
    {
        _lockboxRegistry.Unregister(filledLockbox);
        Pool.Return(filledLockbox);
        _colorKeyCounter.UpdateKeyCounts();

        if (_colorKeyCounter.HasKeys())
        {
            Transform spawnPoint = filledLockbox.transform;
            CreateNew(spawnPoint);
        }

        yield return null;
    }

    private void CreateNew(Transform spawnPoint)
    {
        _colorKeyCounter.UpdateKeyCounts();

        Dictionary<BaseColor, int> colorKeyCounts = _colorKeyCounter.GetPerColor();
        Dictionary<BaseColor, int> lockboxesPerColor = _lockboxCalculator.CalculatePerColor(colorKeyCounts);
        BaseColor newColor = _lockboxColorPicker.GetSingleColor(lockboxesPerColor);
        bool reserved = _colorKeyCounter.Reserve(newColor, _lockboxCalculator.KeysPerLockbox);

        if (reserved)
        {
            Create(spawnPoint, newColor);
        }
    }

    private void Create(Transform spawnPoint, BaseColor color)
    {
        Lockbox lockbox = Pool.Get();
        _lockboxRegistry.Register(lockbox);
        SetInstanceTransform(lockbox, spawnPoint);
        lockbox.Initialize(color);
    }

    private void CreateInactiveMarker(Transform spawnPoint)
    {
        _inactivePrefab = Instantiate(_inactivePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}