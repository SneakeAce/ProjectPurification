using UnityEngine;
using Zenject;

public class SearchTargetSystemInstaller : MonoInstaller
{
    [SerializeField] private EnemySearchTargetConfig _enemyConfig;
    [SerializeField] private TurretSearchTargetConfig _turretConfig;

    public override void InstallBindings()
    {
        BindConfigs();

        BindSearchSystems();
    }

    private void BindConfigs()
    {
        Container.Bind<EnemySearchTargetConfig>().FromInstance(_enemyConfig).AsTransient();
        Container.Bind<TurretSearchTargetConfig>().FromInstance(_turretConfig).AsTransient();
    }

    private void BindSearchSystems()
    {
        Container.Bind<TurretSearchTargetSystem>().AsTransient();
        Container.Bind<EnemySearchTargetSystem>().AsTransient();
    }
}
