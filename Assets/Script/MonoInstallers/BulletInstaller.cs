using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        BindBulletComponents();
    }

    private void BindBulletComponents()
    {
        Container.Bind<IBulletComponent>().To<AttackBullet>().AsTransient();
        Container.Bind<IBulletComponent>().To<MoveBullet>().AsTransient();
    }

}
