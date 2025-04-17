using System;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private SearchTargetSystemConfig _config; 

    public override void InstallBindings()
    {
        Container.Bind<SearchTargetSystemConfig>().FromInstance(_config).AsSingle();

        BindEnemyComponents();
    }
    
    private void BindEnemyComponents()
    {
        Container.Bind<EnemyHealth>().AsTransient();
        Container.Bind<EnemySearchTargetSystem>().AsTransient();
    }
}
