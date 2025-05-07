using Zenject;

public class EntityFactoriesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindEnemyFactory();

        BindBarrierFactory();

        BindTurretFactory();

        BindBulletFactory();

        BindWeaponFactory();

        BindSpawnPointForSpawnerFactory();
    }

    private void BindBulletFactory()
    {
        Container.Bind<IFactory<Bullet, BulletConfig, BulletType>>().To<BulletFactory>().AsSingle();
    }

    private void BindBarrierFactory()
    {
        Container.Bind<IFactory<Barrier, BarrierConfig, BarriersType>>().To<BarrierFactory>().AsSingle();
    }

    private void BindTurretFactory()
    {
        Container.Bind<IFactory<Turret, TurretConfig, TurretType>>().To<TurretFactory>().AsSingle();
    }

    private void BindEnemyFactory()
    {
        Container.Bind<IFactory<EnemyCharacter, EnemyConfig, EnemyType>>().To<EnemyFactory>().AsSingle();

    }

    private void BindWeaponFactory()
    {
        Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle();
    }

    private void BindSpawnPointForSpawnerFactory()
    {
        Container.Bind<ISpawnPointFactory>()
            .To<SpawnPointForSpawnerFactory>()
            .AsSingle();
    }
}
