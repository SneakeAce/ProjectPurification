using UnityEngine;
using Zenject;

public class SpawnPatrolPointsInstaller : MonoInstaller
{
    [SerializeField] private SpawnPatrolPointsConfig _patrolPointsConfig;

    public override void InstallBindings()
    {
        BindSpawnPatrolPointsConfig();

        BindSpawnPatrolPoints();
    }

    private void BindSpawnPatrolPointsConfig()
    {
        Container.Bind<SpawnPatrolPointsConfig>().FromInstance(_patrolPointsConfig).AsSingle();
    }

    private void BindSpawnPatrolPoints()
    {
        Container.Bind<PatrolPointsSpawner>().AsTransient();
    }
}
