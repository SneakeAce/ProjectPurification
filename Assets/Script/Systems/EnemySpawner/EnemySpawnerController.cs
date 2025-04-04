using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawnerController : MonoBehaviour
{
    private EnemySpawner _globalEnemySpawner;
    private EnemySpawner _localEnemySpawner;

    [Inject]
    private void Construct()
    {

    }

}
