using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private LockboxRegistry _lockboxRegistry;
    [SerializeField] private SpawnerLockbox _spawnerLockbox;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<SpawnerKeysCreatedSignal>();

        Container.Bind<SpawnerLockbox>().FromInstance(_spawnerLockbox).AsSingle().NonLazy();
        Container.Bind<LockboxRegistry>().FromInstance(_lockboxRegistry).AsSingle();

        Container.Bind<LaunchingSpawners>().FromComponentInHierarchy().AsSingle();
        Container.Bind<KeyLayer>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SpawnerKeys>().FromComponentInHierarchy().AsSingle();
    }
}