using System;
using UnityEngine;

[Serializable]
public class SpecificationsBarrier
{
    [field: SerializeField] public BarrierType BarrierType { get; private set; }
    [field: SerializeField] public int MaxCountOnCurrentScene { get; private set; }
    [field: SerializeField] public float MaxEndurance { get; private set; }

}
