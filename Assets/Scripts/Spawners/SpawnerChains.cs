public class SpawnerChains : BaseSpawner<Chain>
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Create()
    {
        CreateByPoints();
    }
}