using System.Collections;
using UnityEngine;

public class TurretSearchTargetSystem : SearchTargetSystem
{
    private const int MaxTargetsInBuffer = 16;

    private const float MaxRadiusToAttack = 8f;
    private const float MinDelayToCheck = 0.2f;
    private const float MaxDelayToCheck = 1.0f;

    private float _sqrMaxRadiusToAttack;

    private Collider[] _bufferTargets;

    private ITurret _turret;

    private IEnemy _nearestTarget;

    public TurretSearchTargetSystem(TurretSearchTargetConfig config, 
        CoroutinePerformer coroutinePerformer) : base(coroutinePerformer)
    {
        _radiusSearching = config.RadiusSearching;
        _targetLayerMask = config.TargetLayerMask;
    }

    public void Start(ITurret turret)
    {
        _turret = turret;
        _bufferTargets = new Collider[MaxTargetsInBuffer];

        _sqrMaxRadiusToAttack = MaxRadiusToAttack * MaxRadiusToAttack;

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
        _turret.TurretAttack.SetTarget(_nearestTarget);

        if (_checkDistanceToTargetCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_checkDistanceToTargetCoroutine);
            _checkDistanceToTargetCoroutine = null;
        }

        _checkDistanceToTargetCoroutine = _coroutinePerformer.StartCoroutine(CheckDistanceToTargetJob());
    }

    private void TargetDisapperead()
    {
        _turret.TurretAttack.SetTarget(null);

        if (_searchTargetCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_searchTargetCoroutine);
            _searchTargetCoroutine = null;
        }

        _searchTargetCoroutine = _coroutinePerformer.StartCoroutine(SearchingTargetJob());
    }
}
