using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavioralPattern : MonoBehaviour
{
    [SerializeField] private EnemyCharacter _enemyCharacter;
    [SerializeField] private SearchTarget _searchTarget;
    //[SerializeField] private SpawnPatrolPoints _spawnPatrolPoints;

    private Character _target;
    private List<Transform> _patrolPoints;

    // Сделать обджект пул для точек патруля.
    // Метод в котором при старте случайным образом выбирается поведение.
    // Метод который будет сменять поведение.

    private void Start()
    {
        _searchTarget.TargetFound += OnTargetFound;

        _searchTarget.StartSearchingTarget();
    }

    //private void GetPoint()
    //{
    //    _patrolPoints = new List<Transform>();
    //    _patrolPoints = _spawnPatrolPoints.GetPatrolPoints();
    //}

    private void OnTargetFound()
    {
        _target = _searchTarget.Target;

        _enemyCharacter.SetBehavioralPattern(new MoveToTargetPattern(_enemyCharacter, _target));

        _searchTarget.TargetFound -= OnTargetFound;
    }
}
