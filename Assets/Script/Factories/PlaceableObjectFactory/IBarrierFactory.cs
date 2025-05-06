using UnityEngine;

public interface IBarrierFactory
{
    Barrier Create(Vector3 spawnPosition, BarriersType barrierType,
        Quaternion rotation);
}
