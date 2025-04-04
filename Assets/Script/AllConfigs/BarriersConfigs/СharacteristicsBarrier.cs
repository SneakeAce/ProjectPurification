using System;
using UnityEngine;

[Serializable]
public class CharacteristicsBarrier
{
    [field: SerializeField] public BarriersType BarrierType { get; private set; }
    [field: SerializeField] public int MaxCountOnCurrentScene { get; private set; }
    [field: SerializeField] public float MaxEndurance { get; private set; }

}
