using BaseElements.FolderKey;
using LayersAndGroup;
using UnityEngine;

namespace Spawners.SpawnerKey
{
    public abstract class SpawnerWithKey<TSpawned> : BaseSpawner<TSpawned> 
        where TSpawned : MonoBehaviour
    {
        [SerializeField] private SpawnGroupLocator _groups;
        [SerializeField] private SpawnerKeys _keySpawner;

        public override void Awake()
        {
            base.Awake();
        }

        public virtual void OnDisable()
        {
            foreach (Key key in _keySpawner.SpawnedInstances)
            {
                key.Collected -= OnCollected;
            }

            foreach (TSpawned instance in SpawnedInstances)
            {
                UnsubscribeEvents(instance);
            }
        }

        public override void Create()
        {
            CreateByGroups();

            foreach (Key key in _keySpawner.SpawnedInstances)
            {
                key.Collected += OnCollected;
            }
        }

        public override int GetGroupCount()
        {
            return _groups.SubGroup.Length;
        }

        public override Transform GetPoint(int index)
        {
            return GetSpawnPoint(_groups.SubGroup[index]);
        }

        public override void OnInstanceCreated(TSpawned instance, int index)
        {
            SetGroupIndex(instance, index);
            SubscribeEvents(instance);
        }

        public abstract Transform GetSpawnPoint(SubGroup group);

        public abstract void SetGroupIndex(TSpawned instance, int index);

        public abstract void SubscribeEvents(TSpawned instance);

        public abstract void UnsubscribeEvents(TSpawned instance);

        public abstract void OnCollected(Key key);
    }
}