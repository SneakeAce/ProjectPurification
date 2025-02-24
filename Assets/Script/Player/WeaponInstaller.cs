using UnityEngine;
using Zenject;

public class WeaponInstaller : MonoInstaller
{
    [SerializeField] private PoolCreator _poolCreator;
    [SerializeField] private GameObject _holder;

    public override void InstallBindings()
    {
        Container.Bind<PoolCreator>().FromInstance(_poolCreator).AsSingle();
        Container.Bind<Weapon>().FromComponentInNewPrefab(_holder).AsSingle();
    }
}
