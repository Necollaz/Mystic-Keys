using UnityEngine;
using BaseElements.FolderChisel;

namespace Pools
{
    public class PoolChisels : BasePool<Chisel>
    {
        public PoolChisels(Chisel prefab, int initialSize, Transform parent = null)
            : base(prefab, initialSize, parent)
        {
        }
    }
}