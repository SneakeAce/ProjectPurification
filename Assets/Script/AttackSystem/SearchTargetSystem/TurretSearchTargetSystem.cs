using System.Collections;
using UnityEngine;

public class TurretSearchTargetSystem : SearchTargetSystem
{
    private const int MaxTargetsInBuffer = 16;

    private const float MinDelayToCheck = 0.2f;
    private const float MaxDelayToCheck = 1.0f;

    private float _sqrMaxRadiusToAttack;
    private float _maxRadiusToAttack;

    private Collider[] _bufferTargets;

    private ITurret _turret;

    private IEnemy _nearestTarget;
    private IEnemy _subscribedEnemy;

    public TurretSearchTargetSystem(CoroutinePerformer coroutinePerformer) : base(coroutinePerformer)
    {
    }

    public void Start(ITurret turret, TurretConfig currentTurretConfig)
    {
        _turret = turret;
        _turret.EntityHealth.EntityDied += OntDisable;

        _radiusSearching = currentTurretConfig.AttackCharacteristics.BaseRadiusSearching;
        _targetLayerMask = currentTurretConfig.AttackCharacteristics.TargetLayer;
        _maxRadiusToAttack = currentTurretConfig.AttackCharacteristics.BaseAttackRange;

        _bufferTargets = new Collider[MaxTargetsInBuffer];

        _sqrMaxRadiusToAttack = _maxRadiusToAttack * _maxRadiusToAttack;

        _searchTargetCoroutine = _coroutinePerformer.StartCoroutine(SearchingTargetJob());
    }

    protected override IEnumerator SearchingTargetJob()
    {
        while (_nearestTarget == null)
        {
            yield return new WaitForSeconds(Random.Range(MinDelayToCheck, MaxDelayToCheck));

            int targets = Physics.OverlapSphereNonAlloc(
                _turret.Transform.position, 
                _radiusSearching, 
                _bufferTargets, 
                _targetLayerMask);

            for (int i = 0; i < targets; i++)
            {
                Collider targetCol = _bufferTargets[i];

                bool canMakeTarget = CheckDistanceToTarget(targetCol);

                if (canMakeTarget)
                {
                    _nearestTarget = targetCol.GetComponentInParent<IEnemy>();
                    break;
                }
            }
        }

        TargetFound();
    }

    protected override IEnumerator CheckDistanceToTargetJob()
    {
        while (_nearestTarget != null)
        {
            yield return new WaitForSeconds(MinDelayToCheck);

            bool targetStillClose = CheckDistanceToTarget(_nearestTarget);

            if (targetStillClose == false)
                _nearestTarget = null;
        }

        TargetDisapperead();
    }

    private bool CheckDistanceToTarget(Collider target)
    {
        if (target == false)
            return false;

        float sqrDistance = (target.transform.position - _turret.Transform.position).sqrMagnitude;

        if (sqrDistance <= _sqrMaxRadiusToAttack)
            return true;

        return false;
    }

    private bool CheckDistanceToTarget(IEnemy target)
    {
        if (target == null)
            return false;

        float sqrDistance = (target.Transform.position - _turret.Transform.position).sqrMagnitude;

        if (sqrDistance <= _sqrMaxRadiusToAttack)
            return true;

        return false;
    } 

    private void TargetFound()
    {
        _subscribedEnemy = _nearestTarget;
        _subscribedEnemy.CharacterEnemy.EntityHealth.EntityDied += OnTargetDead;

        _turret.TurretWeapon.SetTarget(_nearestTarget);

        if (_checkDistanceToTargetCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_checkDistanceToTargetCoroutine);
            _checkDistanceToTargetCoroutine = null;
        }

        _checkDistanceToTargetCoroutine = _coroutinePerformer.StartCoroutine(CheckDistanceToTargetJob());
    }

    private void TargetDisapperead()
    {
        _turret.TurretWeapon.ResetTarget(null);

        if (_subscribedEnemy != null)
        {
            _subscribedEnemy.CharacterEnemy.EntityHealth.EntityDied -= OnTargetDead;
            _subscribedEnemy = null;
        }

        if (_searchTargetCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_searchTargetCoroutine);
            _searchTargetCoroutine = null;
        }

        _searchTargetCoroutine = _coroutinePerformer.StartCoroutine(SearchingTargetJob());
    }

    private void OnTargetDead(IEntity enemy)
    {
        if (_nearestTarget == enemy)
            _nearestTarget = null;
    }

    private void OntDisable(IEntity entity)
    {
        StopCoroutine(_searchTargetCoroutine);
        StopCoroutine(_checkDistanceToTargetCoroutine);
    }

    private void StopCoroutine(Coroutine routine)
    {
        if (routine != null)
        {
            _coroutinePerformer.StopCoroutine(routine);
            routine = null;
        }
    }
}
