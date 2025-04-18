using UnityEngine;
using Zenject;

public abstract class PlaceableObject : MonoBehaviour
{
    protected PlaceableObjectConfig _config;

    //[Inject] protected LazyInject<PlaceableObjectHealth> _lazyHealth;
    //protected PlaceableObjectHealth _health;
    public BarriersType BarrierType => _config.Main—haracteristics.BarrierType;

    //private void Construct(PlaceableObjectHealth barrierHealth)
    //{
    //    _health = barrierHealth;
    //}

    public void SetComponents(PlaceableObjectConfig config)
    {
        _config = config;

        //_health = _lazyHealth.Value;
    }
}
