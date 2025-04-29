using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageCalculator
{
    float CalculateDamage(DamageData damage, ArmorData targetArmor);
}
