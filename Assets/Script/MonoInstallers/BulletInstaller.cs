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

        UnityEngine.Debug.Log("BindBulletComponents");
    }

    private void BindBulletFactory()
    {
        UnityEngine.Debug.Log("BindBulletFactory");

        Container.Bind<IFactory<Bullet, BulletType>>().To<BulletFactory>().AsSingle();
    }

}
