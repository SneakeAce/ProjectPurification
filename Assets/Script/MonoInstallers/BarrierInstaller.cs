using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BarrierInstaller : MonoInstaller
{
    [SerializeField] private BarrierConfig _config;

    public override void InstallBindings()
    {
        BindConfig();
    }

    private void BindConfig()
    {
        Container.Bind<BarrierConfig>().FromInstance(_config).AsTransient();
    }

    private void BindHealth()
    {
        Container.Bind<BarrierHealth>().AsTransient();
    }

}
