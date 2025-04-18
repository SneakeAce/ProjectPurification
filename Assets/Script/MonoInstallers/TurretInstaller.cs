using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TurretInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AutomaticTurretAttack>().AsTransient();
    }
}
