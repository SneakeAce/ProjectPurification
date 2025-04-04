using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private EnemyConfig _enemyConfig;

    public override void InstallBindings()
    {
        BindEnemyConfig();

        BindEnemyComponents();
    }

    private void BindEnemyConfig()
    {
        Container.Bind<EnemyConfig>().FromInstance(_enemyConfig).AsTransient();
    }
    
    private void BindEnemyComponents()
    {
        Container.Bind<EnemyHealth>().AsTransient();
    }
}
