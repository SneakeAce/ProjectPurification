using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : IDamageable
{

    private ArmorData _armorData;
    private DamageCalculator _damageCalculator;

    public TurretHealth(TurretConfig config, DamageCalculator damageCalculator)
    {
        _armorData = new ArmorData(ArmorType.LightArmor, config.ArmorCharacteristics.ArmorFactor);

        _damageCalculator = damageCalculator;
    }

    public void TakeDamage(DamageData damage)
    {
        float finalDamage = _damageCalculator.CalculateDamage(damage, _armorData);

        if (finalDamage <= 0)
            return;

        ApplyDamage(finalDamage);
    }

    public void ApplyDamage(float damage)
    {
        


    }

}
