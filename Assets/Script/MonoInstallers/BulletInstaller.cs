using System.Diagnostics;
using Zenject;

public class BulletInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindBulletFactory();

        BindBulletComponents();
    }

    private void BindBulletComponents()
    {
        Container.Bind<AttackBullet>().AsTransient();
        Container.Bind<MoveBullet>().AsTransient();
    }

    private void BindBulletFactory()
    {
        Container.Bind<IFactory<Bullet, BulletConfig, BulletType>>().To<BulletFactory>().AsSingle();
    }

}
