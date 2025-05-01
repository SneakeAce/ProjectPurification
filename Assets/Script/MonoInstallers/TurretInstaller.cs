using Zenject;

public class TurretInstaller : MonoInstaller
{
    public override void InstallBindings()
    {    
        Container.Bind<IDamageCalculator>().To<DamageCalculator>().AsSingle();
    }
}
