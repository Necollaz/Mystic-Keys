using UnityEngine;
using Zenject;

public class InventoryInstaller : MonoInstaller
{
    [SerializeField] private LockboxRegistry _lockboxRegistry;
    [SerializeField] private SpawnerLockbox _spawnerLockbox;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerInput _playerInput;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<SpawnerKeysCreatedSignal>();
        Container.Bind<SpawnerLockbox>().FromInstance(_spawnerLockbox).AsSingle();
        Container.Bind<LockboxRegistry>().FromInstance(_lockboxRegistry).AsSingle();
        Container.Bind<Inventory>().FromInstance(_inventory).AsSingle();
        Container.Bind<PlayerInput>().FromInstance(_playerInput).AsSingle();
        Container.Bind<LaunchingSpawners>().FromComponentInHierarchy().AsSingle();
    }
}