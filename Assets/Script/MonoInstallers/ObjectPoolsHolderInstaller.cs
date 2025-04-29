using UnityEngine;
using Zenject;

public class ObjectPoolsHolderInstaller : MonoInstaller
{
    [SerializeField] private ObjectPoolsHolder _holder;

    private Vector3 _holderSpawnCoordinates = new Vector3(0, 0, 0);

    public override void InstallBindings()
    {
        BindHolder();
    }

    private void BindHolder()
    {
        ObjectPoolsHolder holder = Container.InstantiatePrefabForComponent<ObjectPoolsHolder>(
            _holder,
            _holderSpawnCoordinates,
            Quaternion.identity,
            null);

        Container.Bind<ObjectPoolsHolder>().FromInstance(holder).AsSingle();
    }
}
