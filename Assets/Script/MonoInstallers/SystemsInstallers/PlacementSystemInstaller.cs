using UnityEngine;
using Zenject;

public class PlacementSystemInstaller : MonoInstaller
{
    [SerializeField] private ObjectPlacementSystemsController _placementSystemsController;

    [SerializeField] private BarrierPlacementSystemConfig _barrierSystemConfig;
    [SerializeField] private TurretPlacementSystemConfig _turretSystemConfig; 

    public override void InstallBindings()
    {
        BindPlacementSystemsConfigs();

        BindPlacementSystems();

        BindPlacementSystemsController();
    }

    private void BindPlacementSystemsConfigs()
    {
        Container.Bind<BarrierPlacementSystemConfig>().FromInstance(_barrierSystemConfig).AsTransient();
        Container.Bind<TurretPlacementSystemConfig>().FromInstance(_turretSystemConfig).AsTransient();
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
