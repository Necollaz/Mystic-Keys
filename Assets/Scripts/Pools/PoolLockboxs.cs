using UnityEngine;
using BaseElements.FolderLockbox;

namespace Pools
{
    public class PoolLockboxs : BasePool<Lockbox>
    {
        public PoolLockboxs(Lockbox prefab, int initialSize, Transform parent = null)
            : base(prefab, initialSize, parent)
        {
        }
    }
}