using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBootstrap : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private CreatedPoolEnemySystem _poolEnemySystem;

    public void Initialization()
    {
        _poolEnemySystem.Initialization();
        _spawner.Initialization();
    }
}
