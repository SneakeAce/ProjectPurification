using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private EnemyType _enemyTypeInSpawnPoint;

    private float _radiusSpawning;

    private int _maxEnemyOnScene;
    private int _currentEnemyOnScene;

    public EnemyType EnemyTypeInSpawnPoint => _enemyTypeInSpawnPoint;
    public int MaxEnemyOnScene => _maxEnemyOnScene;
    public int CurrentEnemyOnScene { get => _currentEnemyOnScene; }
    public float RadiusSpawning { get => _radiusSpawning; }

    public void Initialize(SpawnPointConfig config)
    {
        _enemyTypeInSpawnPoint = config.EnemyTypeInSpawnPoint;
        _radiusSpawning = config.RadiusSpawning;
        _maxEnemyOnScene = config.MaxEnemyOnScene;
    }

    public void IncreaseCurrentEnemy(int value)
    {
        _currentEnemyOnScene = _currentEnemyOnScene + value;
    }

    public void DecreaseCurrentEnemy(int value)
    {
        _currentEnemyOnScene = _currentEnemyOnScene - value;
    }
}
