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
    }

    private void BindTurretComponents()
    {
        Container.Bind<TurretHealth>().AsTransient();
    }

    private void BindBarrierComponents()
    {
        Container.Bind<BarrierHealth>().AsTransient();
        Container.Bind<BarrierAttack>().AsTransient();
    }

}
