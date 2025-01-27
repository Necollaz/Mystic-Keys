using System.Linq;
using UnityEngine;
using BaseElements.FolderKey;
using BaseElements.FolderPadlock;
using LayersAndGroup;
using Spawners.SpawnerKey;

namespace Spawners
{
    public class SpawnerPadlocks : SpawnerWithKey<Padlock>
    {
        public override Transform GetSpawnPoint(SubGroup group)
        {
            return group.PadlockSpawnPoint;
        }
        
        public override void SetGroupIndex(Padlock padlock, int index)
        {
            padlock.GroupIndex = index;
        }
        
        public override void SubscribeEvents(Padlock padlock)
        {
            padlock.UnlockCompleted += OnUnlockCompleted;
        }
        
        public override void UnsubscribeEvents(Padlock padlock)
        {
            padlock.UnlockCompleted -= OnUnlockCompleted;
        }
        
        public override void OnCollected(Key key)
        {
            int groupIndex = key.GroupIndex;
            Padlock padlock = SpawnedInstances.FirstOrDefault(padlock => padlock.GroupIndex == groupIndex);
            
            if (padlock != null && !padlock.IsUnlocked)
            {
                padlock.Unlock();
            }
        }
        
        private void OnUnlockCompleted(Padlock padlock)
        {
            padlock.gameObject.SetActive(false);
            Pool.Return(padlock);
        }
    }
}