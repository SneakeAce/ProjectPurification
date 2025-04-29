using System.Collections;
using UnityEngine;

public class EnemySearchTargetSystem : SearchTargetSystem
{
    //Сделать систему приоритетов для врага: Игрок, База, Барьеры, Турели.

    private const int MaxTargetsInBuffer = 1;

    private const float MinDelayToCheck = 0.2f;
    private const float MaxDelayToCheck = 1.0f;

    protected IEnemy _enemy;

    private Collider[] _bufferTargets;

    private BehavioralPatternSwitcher _patternSwitcher;

    private ICharacter _target;

    public EnemySearchTargetSystem(EnemySearchTargetConfig config, 
        CoroutinePerformer coroutinePerformer) : base(coroutinePerformer)
    {
        _radiusSearching = config.RadiusSearching;
        _targetLayerMask = config.TargetLayerMask;
    }

    public void Start(IEnemy enemy)
    {
        _enemy = enemy;
        _patternSwitcher = enemy.BehavioralPatternSwitcher;
        _bufferTargets = new Collider[MaxTargetsInBuffer];

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

                _target = target.gameObject.GetComponentInParent<ICharacter>();

            }

            yield return new WaitForSeconds(MinDelayToCheck);
        }

        TargetFound();
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

        TargetDisapperead();
    }

    private void TargetFound()
    {
        _patternSwitcher.SetBehavioralPattern(MoveTypes.MoveToTarget, _target);

        if (_checkDistanceToTargetCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_checkDistanceToTargetCoroutine);
            _checkDistanceToTargetCoroutine = null;
        }

        _checkDistanceToTargetCoroutine = _coroutinePerformer.StartCoroutine(CheckDistanceToTargetJob());
    }

    private void TargetDisapperead()
    {
        //Debug.Log("TargetDisapperead");
        _patternSwitcher.SetBehavioralPattern(MoveTypes.NoMove);

        if (_searchTargetCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_searchTargetCoroutine);
            _searchTargetCoroutine = null;
        }

        _searchTargetCoroutine = _coroutinePerformer.StartCoroutine(SearchingTargetJob());
    }
}
