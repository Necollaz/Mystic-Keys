using UnityEngine;

namespace Pools
{
    public class ParticleSystemPool : BasePool<ParticleSystem>
    {
        public ParticleSystemPool(ParticleSystem prefab, int initialSize, Transform parent = null) : base(prefab, initialSize, parent) { }
    }
}