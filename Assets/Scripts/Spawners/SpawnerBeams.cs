public class SpawnerBeams : BaseSpawner<Beam>
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