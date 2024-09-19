public class SpawnerChisels : BaseSpawner<Chisel>
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