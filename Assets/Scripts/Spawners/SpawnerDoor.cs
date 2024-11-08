public class SpawnerDoor : BaseSpawner<Door>
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