using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnPoint : MonoBehaviour
{
    private EnemyType _enemyTypeInSpawner;

    private float _radiusSpawning;

    private int _maxEnemyOnScene;
    private int _currentEnemyOnScene;

    public EnemyType EnemyTypeInSpawner => _enemyTypeInSpawner;
    public int MaxEnemyOnScene => _maxEnemyOnScene;
    public int CurrentEnemyOnScene { get => _currentEnemyOnScene; set => _currentEnemyOnScene = value; }
    public float RadiusSpawning { get => _radiusSpawning; }

    public void Initialize(SpawnPointConfig config)
    {
        _enemyTypeInSpawner = config.EnemyTypeInSpawner;
        _radiusSpawning = config.RadiusSpawning;
        _maxEnemyOnScene = config.MaxEnemyOnScene;
    }
}
