using BaseElements.FolderPadlock;
using UnityEngine;

namespace Pools
{
    public class PoolPadlock : BasePool<Padlock>
    {
        public PoolPadlock(Padlock prefab, int initialSize, Transform parent = null)
            : base(prefab, initialSize, parent) { }
    }
}