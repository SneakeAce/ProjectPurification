using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolsBootstrap : MonoBehaviour
{
    [Header("Barriers Prefabs")]
    [SerializeField] private List<PlaceableObject> _placeableObjects;

    [Header("Enmeies Prefabs")]
    [SerializeField] private List<EnemyCharacter> _enemyCharacterObjects;

    private CreatedPoolBarriersSystem _createdPoolBarrierSystem;
    private CreatedPoolEnemiesSystem _poolEnemySystem;

    public CreatedPoolEnemiesSystem PoolEnemySystem => _poolEnemySystem;
    public CreatedPoolBarriersSystem PoolBarrierSystem => _createdPoolBarrierSystem;

    public void Initialization()
    {
        _createdPoolBarrierSystem = new CreatedPoolBarriersSystem(_placeableObjects);
        _createdPoolBarrierSystem.Initialization();

        _poolEnemySystem = new CreatedPoolEnemiesSystem(_enemyCharacterObjects);
        _poolEnemySystem.Initialization();
    }
}
