using UnityEngine;
using Zenject;

public class PlacementSystemInstaller : MonoInstaller
{
    [SerializeField] private ObjectPlacementSystemsController _placementSystemsController;

    [SerializeField] private ObjectPlacementSystemConfig _barrierSystemConfig;
    [SerializeField] private ObjectPlacementSystemConfig _turretSystemConfig;

    public override void InstallBindings()
    {
        BindPlacementSystemsConfigs();

        BindPlacementSystems();

        BindPlacementSystemsController();
    }

    private void BindPlacementSystemsConfigs()
    {
        Container.Bind<ObjectPlacementSystemConfig>().FromInstance(_barrierSystemConfig).AsTransient().NonLazy();
        Container.Bind<ObjectPlacementSystemConfig>().FromInstance(_turretSystemConfig).AsTransient().NonLazy();
    }

    private void BindPlacementSystems()
    {
        Container.Bind<IPlacementSystem>().To<BarrierPlacementSystem>().AsTransient();
        Container.Bind<IPlacementSystem>().To<TurretPlacementSystem>().AsTransient();
    }

    private void BindPlacementSystemsController()
    {
        Container.Bind<ObjectPlacementSystemsController>().FromInstance(_placementSystemsController).AsSingle();
    }
}
