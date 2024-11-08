using System.Linq;
using UnityEngine;

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

    public override void OnKeyCollected(Key key)
    {
        int groupIndex = key.GroupIndex;
        Chisel chisel = SpawnedInstances.FirstOrDefault(chisel => chisel.GroupIndex == groupIndex);

        if (chisel != null)
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