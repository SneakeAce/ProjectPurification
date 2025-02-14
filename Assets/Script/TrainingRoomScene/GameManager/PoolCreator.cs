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

    private CreatedPoolBarriersSystem _createdPoolBarrierSystem;
    private CreatedPoolEnemiesSystem _poolEnemySystem;
    private CreatedPoolTurretsSystem _poolTurretsSystem;

    public CreatedPoolEnemiesSystem PoolEnemySystem => _poolEnemySystem;
    public CreatedPoolBarriersSystem PoolBarrierSystem => _createdPoolBarrierSystem;
    public CreatedPoolTurretsSystem PoolTurretsSystem => _poolTurretsSystem; 


    public void Initialization()
    {
        _createdPoolBarrierSystem = new CreatedPoolBarriersSystem(_placeableObjects);
        _createdPoolBarrierSystem.Initialization();

        _poolEnemySystem = new CreatedPoolEnemiesSystem(_enemyCharacterObjects);
        _poolEnemySystem.Initialization();

        _poolTurretsSystem = new CreatedPoolTurretsSystem(_turretObjects);
        _poolTurretsSystem.Initialization();
    }
}
