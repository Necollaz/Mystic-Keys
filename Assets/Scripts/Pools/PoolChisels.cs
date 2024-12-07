using BaseElements.FolderChisel;
using UnityEngine;

namespace Pools
{
    public class PoolChisel : BasePool<Chisel>
    {
        public PoolChisel(Chisel prefab, int initialSize, Transform parent = null)
            : base(prefab, initialSize, parent) { }

    }
}