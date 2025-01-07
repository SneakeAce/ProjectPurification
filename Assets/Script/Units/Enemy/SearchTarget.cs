using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTarget : MonoBehaviour
{
    private Character _target;
    private Collider[] _targets;

    private float _maxRadiusSearching = 6f;
    private bool _targetIsFound = false;

    public bool TargetIsFound { get => _targetIsFound; }
    public Character Target { get => _target; }

    private void Update()
    {
        if (_target == null)
            SearchingTarget();
    }

    private void SearchingTarget()
    {
        _targets = Physics.OverlapSphere(transform.position, 6f);

        foreach (Collider target in _targets)
        {
            _target = target.gameObject.GetComponent<Character>();
            _targetIsFound = true;
        }
    }
}
