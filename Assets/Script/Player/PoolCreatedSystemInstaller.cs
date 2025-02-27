using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolCreatedSystemInstaller : MonoInstaller
{
    [SerializeField] private PoolCreator _poolCreator;

    public override void InstallBindings()
    {
        Container.Bind<PoolCreator>().FromInstance(_poolCreator).AsSingle();
    }
}
