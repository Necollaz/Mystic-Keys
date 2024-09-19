public class SpawnerPadlocks : BaseSpawner<Padlock>
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