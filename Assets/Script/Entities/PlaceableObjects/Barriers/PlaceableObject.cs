using UnityEngine;
using Zenject;

public abstract class PlaceableObject : MonoBehaviour
{
    protected PlaceableObjectConfig _config;

    public BarriersType BarrierType => _config.Main—haracteristics.BarrierType;

    [Inject]
    private void Construct(PlaceableObjectHealth barrierHealth)
    {
    }

    public void SetComponents(PlaceableObjectConfig config)
    {
        _config = config;
    }
}
