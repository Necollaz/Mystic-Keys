using BaseElements.FolderKey;
using UnityEngine;

namespace Pools
{
    public class PoolKeys : BasePool<Key>
    {
        public PoolKeys(Key prefab, int initialSize, Transform parent = null) 
            : base(prefab, initialSize, parent) { }
    }
}