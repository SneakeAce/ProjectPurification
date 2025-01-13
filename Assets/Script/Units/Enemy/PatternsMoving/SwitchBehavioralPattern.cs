using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SwitchBehavioralPattern : MonoBehaviour
{
    [SerializeField] private EnemyCharacter _enemyCharacter;
    [SerializeField] private SearchTarget _searchTarget;
    //[SerializeField] private SpawnPatrolPoints _spawnPatrolPoints;

    private float _timeToChoose = 2f;

    private Character _target;
    private List<Transform> _patrolPoints;
    private Coroutine _randomlyChooseMovePatterCoroutine;

    // ������� ������� ��� ��� ����� �������.
    // ����� � ������� ��� ������ ��������� ������� ���������� ���������.
    // ����� ������� ����� ������� ���������.

    private void Awake()
    {
        _enemyCharacter.SetBehavioralPattern(new NoMovePattern());

        _randomlyChooseMovePatterCoroutine = StartCoroutine(RandomlyChooseMovePatternJob());
    }

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

    private IEnumerator RandomlyChooseMovePatternJob()
    {
        yield return new WaitForSeconds(_timeToChoose);


    }
}
