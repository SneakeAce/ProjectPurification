using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBootstrap : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;

    public void Initialization()
    {
        _spawner.Initialization();
    }
}
