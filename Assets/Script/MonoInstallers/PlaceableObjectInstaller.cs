using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlaceableObjectInstaller : MonoInstaller
{
    [SerializeField] private PlaceableObjectConfig _config;

    public override void InstallBindings()
    {
        BindConfig();
    }

    private void BindConfig()
    {
        Container.Bind<PlaceableObjectConfig>().FromInstance(_config).AsTransient();
    }

    private void BindHealth()
    {
        Container.Bind<PlaceableObjectHealth>().AsTransient();
    }

}
