public class SpawnerBeams : BaseSpawner<Beam>
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