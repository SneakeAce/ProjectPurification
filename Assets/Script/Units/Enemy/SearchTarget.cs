using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTarget : MonoBehaviour
{
    [SerializeField] private float _maxRadiusSearching;
    [SerializeField] private LayerMask _targetLayerMask;
    private Character _target;
    private Collider[] _targets;
    private Coroutine _searchTargetCoroutine;

    private bool _targetIsFound = false;

    public bool TargetIsFound { get => _targetIsFound; }
    public Character Target { get => _target; }

    public event Action TargetFound;

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
            _targets = Physics.OverlapSphere(transform.position, _maxRadiusSearching, _targetLayerMask);

            foreach (Collider target in _targets)
            {
                _target = target.gameObject.GetComponent<Character>();
                _targetIsFound = true;

                TargetFound?.Invoke();
            }

            yield return null;
        }

        StopCoroutine(SearchingTargetJob());
        _searchTargetCoroutine = null;
    }
}
