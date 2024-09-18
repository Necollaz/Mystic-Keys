using UnityEngine;

public class PoolBeams : BasePool<Beam>
{
    public PoolBeams(Beam prefab, int initialSize, Transform parent = null)
        : base(prefab, initialSize, parent) { }
}