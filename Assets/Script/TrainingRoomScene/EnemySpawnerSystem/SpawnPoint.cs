using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private EnemyType _enemyTypeInSpawner;
    [SerializeField] private float _radiusSpawning;
    [SerializeField] private int _maxEnemyOnScene;
    private int _currentEnemyOnScene;
    public EnemyType EnemyTypeInSpawner => _enemyTypeInSpawner;

    public int MaxEnemyOnScene => _maxEnemyOnScene;
    public int CurrentEnemyOnScene { get => _currentEnemyOnScene; set => _currentEnemyOnScene = value; }
    public float RadiusSpawning { get => _radiusSpawning; }

    public void Initialization()
    {

    }
}
