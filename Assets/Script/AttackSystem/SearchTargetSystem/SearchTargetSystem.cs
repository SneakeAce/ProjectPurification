using System.Collections;
using UnityEngine;

public abstract class SearchTargetSystem
{
    protected float _radiusSearching;

    protected LayerMask _targetLayerMask;

    protected CoroutinePerformer _coroutinePerformer;

    protected Coroutine _searchTargetCoroutine;
    protected Coroutine _checkDistanceToTargetCoroutine;

    public SearchTargetSystem(CoroutinePerformer coroutinePerformer)
    {
        _coroutinePerformer = coroutinePerformer;
    }

    protected abstract IEnumerator SearchingTargetJob();
    protected abstract IEnumerator CheckDistanceToTargetJob();

}
