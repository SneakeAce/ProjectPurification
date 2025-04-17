using System.Collections;
using UnityEngine;

public class EnemySearchTargetSystem : SearchTargetSystem
{
    //Сделать систему приоритетов для врага: Игрок, База, Барьеры, Турели.

    private const int MaxTargetInBuffer = 1;

    private const float MinDelayToCheck = 0.2f;
    private const float MaxDelayToCheck = 1.0f;

    protected IEnemy _character;

    private Collider[] _bufferTargets;

    private BehavioralPatternSwitcher _patternSwitcher;

    private Character _target;

    public EnemySearchTargetSystem(SearchTargetSystemConfig config, CoroutinePerformer coroutinePerformer) : base(config, coroutinePerformer)
    {
    }

    public void Start(IEnemy character)
    {
        _character = character;
        _patternSwitcher = character.BehavioralPatternSwitcher;
        _bufferTargets = new Collider[MaxTargetInBuffer];

        _searchTargetCoroutine = _coroutinePerformer.StartCoroutine(SearchingTargetJob());
    }

    protected override IEnumerator SearchingTargetJob()
    {
        while (_target == null)
        {
            yield return new WaitForSeconds(Random.Range(MinDelayToCheck, MaxDelayToCheck));

            int targets = Physics.OverlapSphereNonAlloc(_character.Transform.position, _radiusSearching,
                _bufferTargets, _targetLayerMask);

            for (int i = 0; i < targets; i++)
            {
                Collider target = _bufferTargets[i];

                _target = target.gameObject.GetComponent<Character>();
                Debug.Log("_target = " + _target);
            }

            yield return new WaitForSeconds(MinDelayToCheck);
        }

        TargetFound();
    }

    protected override IEnumerator TrackingTargetJob()
    {
        while (_target != null)
        {
            float sqrDistance = (_target.transform.position - _character.Transform.position).sqrMagnitude;

            if (sqrDistance > _radiusSearching * _radiusSearching)
                _target = null;

            Debug.Log("TrackingTargetJob");

            yield return new WaitForSeconds(MinDelayToCheck);
        }

        TargetDisapperead();
    }

    protected override void TargetFound()
    {
        _patternSwitcher.SetBehavioralPattern(MoveTypes.MoveToTarget, _target);

        if (_trackingTargetCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_trackingTargetCoroutine);
            _trackingTargetCoroutine = null;
        }

        _trackingTargetCoroutine = _coroutinePerformer.StartCoroutine(TrackingTargetJob());
    }

    protected override void TargetDisapperead()
    {
        Debug.Log("TargetDisapperead");
        _patternSwitcher.SetBehavioralPattern(MoveTypes.NoMove);

        if (_searchTargetCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_searchTargetCoroutine);
            _searchTargetCoroutine = null;
        }

        _searchTargetCoroutine = _coroutinePerformer.StartCoroutine(SearchingTargetJob());
    }
}
