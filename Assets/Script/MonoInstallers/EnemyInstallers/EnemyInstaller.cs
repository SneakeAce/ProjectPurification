using System;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindEnemyComponents();
    }
    
    private void BindEnemyComponents()
    {
        Container.Bind<EnemyHealth>().AsTransient();
        //Container.Bind<EnemySearchTargetSystem>().AsTransient();
    }
}
