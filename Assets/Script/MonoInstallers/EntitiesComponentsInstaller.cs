using Zenject;

public class EntitiesComponentsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindCharacterComponents();
        BindEnemyComponents();
        BindTurretComponents();
        BindBarrierComponents();
    }

    private void BindCharacterComponents()
    {
        Container.Bind<CharacterHealth>().AsTransient();
    }

    private void BindEnemyComponents()
    {
        Container.Bind<EnemyHealth>().AsTransient();
        Container.Bind<EnemySearchTargetSystem>().AsTransient();
    }

    private void BindTurretComponents()
    {
        Container.Bind<TurretHealth>().AsTransient();
        Container.Bind<TurretSearchTargetSystem>().AsTransient();

        Container.Bind<AutomaticTurretWeapon>().AsTransient();
    }

    private void BindBarrierComponents()
    {
        Container.Bind<BarrierHealth>().AsTransient();
        Container.Bind<BarrierAttack>().AsTransient();
    }

}
