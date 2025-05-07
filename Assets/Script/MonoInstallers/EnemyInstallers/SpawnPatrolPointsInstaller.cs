using UnityEngine;
using Zenject;

public class SpawnPatrolPointsInstaller : MonoInstaller
{
    [SerializeField] private SpawnPatrolPointsConfig _patrolPointsConfig;
    [SerializeField] private ParentForPatrolPointsHolder _parentForPatrolPointsHolderPrefab;

    private readonly Vector3 _coordinateSpawnParent = new Vector3(0, 0, 0);

    public override void InstallBindings()
    {
        BindParentForPatrolPointsHolder();

        BindSpawnPatrolPointsConfig();

        BindSpawnPatrolPoints();
    }

    private void BindParentForPatrolPointsHolder()
    {
        ParentForPatrolPointsHolder holder = Container.
            InstantiatePrefabForComponent<ParentForPatrolPointsHolder>(
            _parentForPatrolPointsHolderPrefab,
            _coordinateSpawnParent,
            Quaternion.identity,
            null);

        Container.Bind<ParentForPatrolPointsHolder>().FromInstance(holder).AsSingle();
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
