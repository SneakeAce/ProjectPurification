using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySearchTargetSystem : SearchTargetSystem
{
    //Сделать систему приоритетов для врага: Игрок, База, Барьеры, Турели.

    private const int MaxTargetsInBuffer = 1;

    private const float MinDelayToCheck = 0.2f;
    private const float MaxDelayToCheck = 1.0f;

    protected IEnemy _enemy;

    private Collider[] _bufferTargets;

    private IEntity _target;
    private IEntity _subscribedEntity;

    public EnemySearchTargetSystem(CoroutinePerformer coroutinePerformer) : base(coroutinePerformer)
    {
    }

    public event Action<IEntity> TargetFound;
    public event Action TargetDisapperead;

    public void Start(IEnemy enemy, EnemyConfig config)
    {
        _enemy = enemy;
        _bufferTargets = new Collider[MaxTargetsInBuffer];

        _radiusSearching = config.AttackCharacteristics.BaseSearchTargetRadius;
        _targetLayerMask = config.AttackCharacteristics.TargetsLayer;

        _searchTargetCoroutine = _coroutinePerformer.StartCoroutine(SearchingTargetJob());
    }

    protected override IEnumerator SearchingTargetJob()
    {
        while (_target == null)
        {
            if (_enemy.Transform.gameObject.activeSelf == false)
                yield break;

            yield return new WaitForSeconds(Random.Range(MinDelayToCheck, MaxDelayToCheck));

            int targets = Physics.OverlapSphereNonAlloc(
                _enemy.Transform.position, 
                _radiusSearching,
                _bufferTargets, 
                _targetLayerMask);

            for (int i = 0; i < targets; i++)
            {
                Collider target = _bufferTargets[i];

                if (target.gameObject.TryGetComponent<IEntity>(out IEntity tar))
                    _target = tar;
            }

            yield return new WaitForSeconds(MinDelayToCheck);
        }

        OnTargetAcquired();
    }

    protected override IEnumerator CheckDistanceToTargetJob()
    {
        while (_target != null)
        {
            if (_enemy.Transform.gameObject.activeSelf == false)
                yield break;
            float sqrDistance = (_target.Transform.position - _enemy.Transform.position).sqrMagnitude;

            if (sqrDistance > _radiusSearching * _radiusSearching)
                _target = null;

           // Debug.Log("EnemySearch / TrackingTargetJob");

            yield return new WaitForSeconds(MinDelayToCheck);
        }

        OnTargetLost();
    }

    private void OnTargetAcquired()
    {
        _subscribedEntity = _target;
        _subscribedEntity.EntityHealth.EntityDied += OnTargetDead;

        _enemy.BehavioralPatternSwitcher.SetBehavioralPattern(MoveTypes.MoveToTarget, _target);

        TargetFound?.Invoke(_target);

        if (_checkDistanceToTargetCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_checkDistanceToTargetCoroutine);
            _checkDistanceToTargetCoroutine = null;
        }

        _checkDistanceToTargetCoroutine = _coroutinePerformer.StartCoroutine(CheckDistanceToTargetJob());
    }

    private void OnTargetLost()
    {
        _enemy.BehavioralPatternSwitcher.SetBehavioralPattern(MoveTypes.NoMove);
        
        TargetDisapperead?.Invoke();

        if (_subscribedEntity != null)
        {
            _subscribedEntity.EntityHealth.EntityDied -= OnTargetDead;
            _subscribedEntity = null;
        }

        if (_searchTargetCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_searchTargetCoroutine);
            _searchTargetCoroutine = null;
        }

        _searchTargetCoroutine = _coroutinePerformer.StartCoroutine(SearchingTargetJob());
    }

    private void OnTargetDead(IEntity entity)
    {
        if (_target == entity)
            _target = null;
    }
}
