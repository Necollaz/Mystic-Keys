using BaseElements.FolderPadlock;
using UnityEngine;

namespace Pools
{
    public class PoolPadlocks : BasePool<Padlock>
    {
        public PoolPadlocks(Padlock prefab, int initialSize, Transform parent = null) 
            : base(prefab, initialSize, parent) { }
    }
}