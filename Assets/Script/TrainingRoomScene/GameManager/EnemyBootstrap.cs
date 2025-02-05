using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBootstrap : MonoBehaviour
{
    [SerializeField] private EnemySpawner _globalSpawner;
    [SerializeField] private EnemySpawner _localSpawner;
    [SerializeField] private CreatedPoolEnemySystem _poolEnemySystem;

    public void Initialization()
    {
        _poolEnemySystem.Initialization();
        _globalSpawner.Initialization();
        _localSpawner.Initialization();
    }
}
