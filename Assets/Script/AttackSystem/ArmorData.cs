using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ArmorData
{
    public ArmorData(ArmorType armorType, float armorFactor)
    {
        ArmorType = armorType;
        ArmorFactor = armorFactor;
    }

    public ArmorType ArmorType { get; }
    public float ArmorFactor { get; }
}
