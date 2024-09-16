using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private BaseElementsPool _elementsPool;
    [SerializeField] private Transform[] _keySpawnPoints;
    [SerializeField] private Transform[] _padlockSpawnPoints;
    [SerializeField] private Transform[] _chiselSpawnPoints;
    [SerializeField] private Transform[] _beamSpawnPoints;

    private void Start()
    {
        SpawnAllElements();
    }

    private void SpawnAllElements()
    {
        Spawn(_keySpawnPoints, _elementsPool.GetKey);
        Spawn(_padlockSpawnPoints, _elementsPool.GetPadlock);
        Spawn(_chiselSpawnPoints, _elementsPool.GetChisel);
        Spawn(_beamSpawnPoints, _elementsPool.GetBeam);
    }

    private void Spawn<T>(Transform[] spawnPoints, Func<T> getItem) where T : MonoBehaviour
    {
        foreach (var point in spawnPoints)
        {
            var item = getItem.Invoke();

            if (item != null)
            {
                item.transform.SetPositionAndRotation(point.position, point.rotation);
                item.transform.localScale = point.localScale;
            }
        }
    }
}
