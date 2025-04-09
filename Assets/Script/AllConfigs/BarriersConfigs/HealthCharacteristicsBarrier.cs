using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealthCharacteristicsBarrier
{
    [field: SerializeField] public ArmorType ArmorType { get; private set; }
    [field: SerializeField] public float BaseEndurance { get; private set; }
}
