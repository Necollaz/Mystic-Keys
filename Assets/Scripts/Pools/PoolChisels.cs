using BaseElements.FolderChisel;
using UnityEngine;

namespace Pools
{
    public class PoolChisels : BasePool<Chisel>
    {
        public PoolChisels(Chisel prefab, int initialSize, Transform parent = null) 
            : base(prefab, initialSize, parent) { }
    }
}