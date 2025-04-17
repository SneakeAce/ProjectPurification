using System.Collections;
using UnityEngine;

public abstract class SearchTargetSystem
{
    protected float _radiusForStartSearching;
    protected float _radiusSearching;

    protected LayerMask _targetLayerMask;

    protected CoroutinePerformer _coroutinePerformer;

    protected Coroutine _searchTargetCoroutine;
    protected Coroutine _trackingTargetCoroutine;

    public SearchTargetSystem(SearchTargetSystemConfig config, CoroutinePerformer coroutinePerformer)
    {
        _radiusSearching = config.RadiusSearching;
        _targetLayerMask = config.TargetLayerMask;

        _coroutinePerformer = coroutinePerformer;
    }

    protected abstract IEnumerator SearchingTargetJob();
    protected abstract IEnumerator TrackingTargetJob();

    protected abstract void TargetFound();
    protected abstract void TargetDisapperead();

    protected void StartSearchingTarget()
    {
        if (_searchTargetCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_searchTargetCoroutine);
            _searchTargetCoroutine = null;
        }

        _searchTargetCoroutine = _coroutinePerformer.StartCoroutine(SearchingTargetJob());
    }

}
