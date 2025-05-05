using UnityEngine;

public class DamageCalculator : IDamageCalculator
{
    private const float DamageNormalizationFactor = 1f;
    private const float ArmorReductionScale = 85f;

    private IDamageCoefficientProvider _damageCoefficientProvider;

    public DamageCalculator(IDamageCoefficientProvider damageCoefficientProvider)
    {
        _damageCoefficientProvider = damageCoefficientProvider;
    }

    public float CalculateDamage(DamageData damage, ArmorData targetArmor)
    {
        float coefficientDamage = _damageCoefficientProvider.GetCoefficient(damage.AttackType, targetArmor.ArmorType);

        float coefficientReductionDamage = DamageNormalizationFactor - Mathf.Exp(-targetArmor.ArmorValue / ArmorReductionScale);

        float damageAfterApplyingCoefficientReductionDamage = damage.Damage * (DamageNormalizationFactor - coefficientReductionDamage);

        float finalDamage = damageAfterApplyingCoefficientReductionDamage * coefficientDamage;

        if (finalDamage <= 0)
            return 0;

        return finalDamage;
    }
}
