using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBootstrap : MonoBehaviour
{
    [SerializeField] private EnemySpawner _globalSpawner;
    [SerializeField] private EnemySpawner _localSpawner;

    public void Initialization()
    {
        _globalSpawner.Initialization();
        _localSpawner.Initialization();
    }
}
