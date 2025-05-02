using UnityEngine;
using Zenject;

public class DamageSystemInstaller : MonoInstaller
{
    [SerializeField] private EnemySearchTargetConfig _enemyConfig;
    [SerializeField] private TurretSearchTargetConfig _turretConfig;

    [SerializeField] private TextAsset _damageMatrixCsvFile;

    public override void InstallBindings()
    {
        BindConfigs();

        BindDamageCoefficientProvider();

        BindDamageCalculator();
    }

    private void BindConfigs()
    {
        Container.Bind<EnemySearchTargetConfig>().FromInstance(_enemyConfig).AsTransient();
        Container.Bind<TurretSearchTargetConfig>().FromInstance(_turretConfig).AsTransient();
    }

    private void BindDamageCalculator()
    {
        Container.Bind<IDamageCalculator>().To<DamageCalculator>().AsSingle();
    }

    private void BindDamageCoefficientProvider()
    {
        Container.Bind<IDamageCoefficientProvider>()
            .To<DamageCoefficientProvider>()
            .AsSingle()
            .WithArguments(_damageMatrixCsvFile).NonLazy();
    }
}
