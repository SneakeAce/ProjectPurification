using System.Collections;
using UnityEngine;
using Zenject;

public class SearchTargetSystem : MonoBehaviour
{
    private float _maxRadiusSearching;
    private LayerMask _targetLayerMask;

    private BehavioralPatternSwitcher _patternSwitcher;

    private Character _target;
    private Coroutine _searchTargetCoroutine;
    private Coroutine _trackingTargetCoroutine;

    [Inject]
    private void Construct(SearchTargetSystemConfig config)
    {
        _maxRadiusSearching = config.MaxRadiusSearching;
        _targetLayerMask = config.TargetLayerMask;
    }

    public void StartSearchingTarget()
    {
        if (_searchTargetCoroutine != null)
        {
            StopCoroutine(_searchTargetCoroutine);
            _searchTargetCoroutine = null;
        }

        _searchTargetCoroutine = StartCoroutine(SearchingTargetJob());
    }

    private IEnumerator SearchingTargetJob()
    {
        while (_target == null)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, _maxRadiusSearching, _targetLayerMask);

            foreach (Collider target in targets)
            {
                _target = target.gameObject.GetComponent<Character>();
            }

            yield return null;
        }

        TargetFound();
    }

    private IEnumerator TrackingTargetJob()
    {
        while (_target != null)
        {
            if (Vector3.Distance(transform.position, _target.transform.position) > _maxRadiusSearching)
            {
                _target = null;
            }

            yield return null;
        }

        TargetDisapperead();
    }

    private void TargetFound()
    {
        _patternSwitcher.SetBehavioralPattern(MoveTypes.MoveToTarget, _target);

        if (_trackingTargetCoroutine != null)
        {
            StopCoroutine(_trackingTargetCoroutine);
            _trackingTargetCoroutine = null;
        }

        _trackingTargetCoroutine = StartCoroutine(TrackingTargetJob());
    }

    private void TargetDisapperead()
    {
        _patternSwitcher.SetBehavioralPattern(MoveTypes.NoMove);

        if (_searchTargetCoroutine != null)
        {
            StopCoroutine(_searchTargetCoroutine);
            _searchTargetCoroutine = null;
        }

        _searchTargetCoroutine = StartCoroutine(SearchingTargetJob());
    }
}
