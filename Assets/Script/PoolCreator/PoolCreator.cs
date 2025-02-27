using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCreator : MonoBehaviour
{
    [Header("Barriers Prefabs")]
    [SerializeField] private List<PlaceableObject> _placeableObjects;

    [Header("Enmeies Prefabs")]
    [SerializeField] private List<EnemyCharacter> _enemyCharacterObjects;

    [Header("Turrets Prefabs")]
    [SerializeField] private List<Turret> _turretObjects;

    [Header("Bullets Prefabs")]
    [SerializeField] private List<Bullet> _bulletObjects;

    private CreatedPoolBarriersSystem _createdPoolBarrierSystem;
    private CreatedPoolEnemiesSystem _poolEnemySystem;
    private CreatedPoolTurretsSystem _poolTurretsSystem;
    private CreatedPoolBulletsSystem _poolBulletsSystem;

    public CreatedPoolEnemiesSystem PoolEnemySystem => _poolEnemySystem;
    public CreatedPoolBarriersSystem PoolBarrierSystem => _createdPoolBarrierSystem;
    public CreatedPoolTurretsSystem PoolTurretsSystem => _poolTurretsSystem;
    public CreatedPoolBulletsSystem PoolBulletsSystem => _poolBulletsSystem;

    public void Initialization()
    {
        _createdPoolBarrierSystem = new CreatedPoolBarriersSystem(_placeableObjects);
        _createdPoolBarrierSystem.Initialization();

        _poolEnemySystem = new CreatedPoolEnemiesSystem(_enemyCharacterObjects);
        _poolEnemySystem.Initialization();

        _poolTurretsSystem = new CreatedPoolTurretsSystem(_turretObjects);
        _poolTurretsSystem.Initialization();

        _poolBulletsSystem = new CreatedPoolBulletsSystem(_bulletObjects);
        _poolBulletsSystem.Initialization();
    }
}
