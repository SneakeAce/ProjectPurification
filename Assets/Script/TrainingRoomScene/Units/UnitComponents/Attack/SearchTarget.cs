using System;
using System.Collections;
using UnityEngine;

public class SearchTarget : MonoBehaviour
{
    [SerializeField] private float _maxRadiusSearching;
    [SerializeField] private LayerMask _targetLayerMask;

    private Character _target;
    private Coroutine _searchTargetCoroutine;
    private Coroutine _trackingTargetCoroutine;

    private bool _targetIsFound = false;

    public bool TargetIsFound { get => _targetIsFound; }
    public Character Target { get => _target; }

    public event Action TargetFound;
    public event Action TargetDisappeared;

    public void StartSearchingTarget()
    {
        if (_searchTargetCoroutine != null)
        {
            StopCoroutine(SearchingTargetJob());
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
                _targetIsFound = true;

                TargetFound?.Invoke();
            }

            yield return null;
        }

        if (_trackingTargetCoroutine != null)
        {
            StopCoroutine(TrackingTargetJob());
            _trackingTargetCoroutine = null;
        }

        _trackingTargetCoroutine = StartCoroutine(TrackingTargetJob());

        StopCoroutine(SearchingTargetJob());
        _searchTargetCoroutine = null;
    }

    private IEnumerator TrackingTargetJob()
    {
        while (_target != null)
        {
            if (Vector3.Distance(transform.position, _target.transform.position) > _maxRadiusSearching)
            {
                _target = null;

                TargetDisappeared?.Invoke();
            }

            yield return null;
        }

        StopCoroutine(TrackingTargetJob());
        _trackingTargetCoroutine = null;
    }
}
