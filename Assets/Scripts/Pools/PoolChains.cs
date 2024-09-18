using UnityEngine;

public class PoolChains : BasePool<Chain>
{
    public PoolChains(Chain prefab, int initialSize, Transform parent = null)
        : base(prefab, initialSize, parent) { }
}