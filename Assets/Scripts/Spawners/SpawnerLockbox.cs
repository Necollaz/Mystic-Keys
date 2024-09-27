using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerLockbox : BaseSpawner<Lockbox>
{
    [SerializeField] private SpawnerKeys _keysSpawner;
    [SerializeField] private int _availableSpawnPoints = 2;

    private LockboxCalculator _lockboxCalculator;
    private LockboxColorPicker _lockboxColorPicker;
    private SpawnIndexSelector _spawnIndexSelector;

    public List<BaseColor> SpawnedLockboxColors { get; private set; } = new List<BaseColor>();

    public override void Awake()
    {
        base.Awake();
        _lockboxCalculator = new LockboxCalculator();
        _lockboxColorPicker = new LockboxColorPicker();
        _spawnIndexSelector = new SpawnIndexSelector();
    }

    public override void Spawn()
    {
        Create();
    }

    private void Create()
    {
        SpawnedLockboxColors.Clear();

        var activeKeys = _keysSpawner.GetActiveKeys().ToList();
        var colorKeyCountsDict = new Dictionary<BaseColor, int>();

        foreach (var key in activeKeys)
        {
            if (!colorKeyCountsDict.ContainsKey(key.Color))
            {
                colorKeyCountsDict[key.Color] = 0;
            }

            colorKeyCountsDict[key.Color]++;
        }

        Dictionary<BaseColor, int> lockboxesPerColor = _lockboxCalculator.CalculatePerColor(colorKeyCountsDict);

        int totalLockboxesNeeded = lockboxesPerColor.Values.Sum();
        int totalAvailableLockboxes = Mathf.Min(totalLockboxesNeeded, _availableSpawnPoints, SpawnPoints.Length);

        List<BaseColor> lockboxColorsToSpawn = _lockboxColorPicker.GetColors(lockboxesPerColor, totalAvailableLockboxes);
        List<int> spawnIndices = _spawnIndexSelector.GetIndices(totalAvailableLockboxes, _availableSpawnPoints, SpawnPoints.Length);

        for (int i = 0; i < lockboxColorsToSpawn.Count; i++)
        {
            BaseColor color = lockboxColorsToSpawn[i];
            int spawnIndex = spawnIndices[i];
            Lockbox lockbox = Pool.Get();
            Transform spawnPoint = SpawnPoints[spawnIndex];

            SetInstanceTransform(lockbox, spawnPoint);
            lockbox.Initialize(color);
            lockbox.OnLockboxFilled += Filled;
            SpawnedLockboxColors.Add(color);
        }
    }

    private void Filled(Lockbox lockbox)
    {
        lockbox.OnLockboxFilled -= Filled;
        Pool.Return(lockbox);
    }
}