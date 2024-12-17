using System.Collections.Generic;
using System.Linq;
using BaseElements.FolderChisel;
using BaseElements.FolderKey;
using LayersAndGroup;
using Spawners.SpawnerKey;
using UnityEngine;

namespace Spawners
{
    public class SpawnerChisels : SpawnerWithKey<Chisel>
    {
        public override Transform GetSpawnPoint(SubGroup group)
        {
            return group.ChiselSpawnPoint;
        }

        public override void SetGroupIndex(Chisel chisel, int index)
        {
            chisel.GroupIndex = index;
        }

        public override void SubscribeEvents(Chisel chisel)
        {
            chisel.PullOutComplete += OnPullOutCompleted;
        }

        public override void UnsubscribeEvents(Chisel chisel)
        {
            chisel.PullOutComplete -= OnPullOutCompleted;
        }

        public override void OnCollected(Key key)
        {
            int groupIndex = key.GroupIndex;

            List<Chisel> chiselsInGroup = SpawnedInstances.Where(chisel => chisel.GroupIndex == groupIndex).ToList();

            foreach (Chisel chisel in chiselsInGroup)
            {
                chisel.PullOut();
            }
        }

        private void OnPullOutCompleted(Chisel chisel)
        {
            chisel.gameObject.SetActive(false);
            Pool.Return(chisel);
        }
    }
}