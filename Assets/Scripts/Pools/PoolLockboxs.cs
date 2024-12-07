using BaseElements.FolderLockbox;
using UnityEngine;

namespace Pools
{
    public class PoolLockbox : BasePool<Lockbox>
    {
        public PoolLockbox(Lockbox prefab, int initialSize, Transform parent = null)
            : base(prefab, initialSize, parent) { }
    }
}