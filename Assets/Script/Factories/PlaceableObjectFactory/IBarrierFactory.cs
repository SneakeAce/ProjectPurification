using UnityEngine;

public interface IBarrierFactory
{
    PlaceableObject Create(Vector3 spawnPosition, BarriersType barrierType,
        Quaternion rotation);
}
