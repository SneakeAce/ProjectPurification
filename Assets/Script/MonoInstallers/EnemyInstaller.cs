using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject; 

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private EnemyConfig _enemyConfig;

    public override void InstallBindings()
    {
        base.InstallBindings();
    }

    
}
