using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavioralPattern : MonoBehaviour
{
    // Сделать обджект пул для точек патруля.
    // Метод в котором при старте случайным образом выбирается поведение.
    // Метод который будет сменять поведение.

   //[SerializeField] private EnemyCharacter _enemyCharacter;
    [SerializeField] private SearchTarget _searchTarget;

    private Character _target;
    private EnemyMovementStrategyFactory _movementFactory;
    private List<Vector3> _patrolPoints = new List<Vector3>();

    public void SetBehavioralPattern(EnemyCharacter enemy)
    {
        _movementFactory = new EnemyMovementStrategyFactory(_target);

        _movementFactory.Get(MoveTypes.NoMove, enemy);

        StartSearchingTarget(enemy);
    }

    private void StartSearchingTarget(EnemyCharacter enemy)
    {
        _searchTarget.TargetFound += OnTargetFound;

        _searchTarget.StartSearchingTarget(enemy);
    }

    private void OnTargetFound(EnemyCharacter enemy)
    {
        _target = _searchTarget.Target;

        enemy.SetBehavioralPattern(new MoveToTargetPattern(enemy, _target));

        _searchTarget.TargetFound -= OnTargetFound;
    }


    /*==================================DON'T USE YET======================================================================================*/

    /*
    private Coroutine _randomlyChooseMovePatterCoroutine;

    [SerializeField] private SpawnPatrolPoints _spawnPatrolPoints;

    private float _timeToChoose = 2f;

     _randomlyChooseMovePatterCoroutine = StartCoroutine(RandomlyChooseMovePatternJob());
    */

    /* Other Methods
      
    private void GetPoint()
    {
        _patrolPoints = new List<Transform>();
        _patrolPoints = _spawnPatrolPoints.GetPatrolPoints();
    }

    private IEnumerator RandomlyChooseMovePatternJob()
    {
        yield return new WaitForSeconds(_timeToChoose);


    }

    */
}
