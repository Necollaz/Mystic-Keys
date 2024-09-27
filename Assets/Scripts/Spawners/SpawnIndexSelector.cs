using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnIndexSelector
{
    public List<int> GetIndices(int totalAvailableLockboxes, int availableSpawnPoints, int spawnPointsLength)
    {
        List<int> spawnIndices = Enumerable.Range(0, spawnPointsLength).ToList();

        if (spawnIndices.Count > availableSpawnPoints)
        {
            spawnIndices = spawnIndices.Take(availableSpawnPoints).ToList();
        }

        spawnIndices = spawnIndices.OrderBy(index => Random.value).ToList();

        if (spawnIndices.Count > totalAvailableLockboxes)
        {
            spawnIndices = spawnIndices.Take(totalAvailableLockboxes).ToList();
        }

        return spawnIndices;
    }
}