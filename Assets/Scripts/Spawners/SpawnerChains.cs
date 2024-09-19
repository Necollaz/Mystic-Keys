public class SpawnerChains : BaseSpawner<Chain>
{
    private void Start()
    {
        Spawn();
    }

    public override void Spawn()
    {
        DefaultSpawn();
    }
}