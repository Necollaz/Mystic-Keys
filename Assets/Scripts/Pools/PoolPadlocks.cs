using UnityEngine;

public class PoolPadlock : BasePool<Padlock>
{
    public PoolPadlock(Padlock prefab, int initialSize, Transform parent = null)
        : base(prefab, initialSize, parent) { }
}