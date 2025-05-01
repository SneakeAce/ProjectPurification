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
        UnityEngine.Debug.Log("coefficientDamage Damage: " + coefficientDamage);

        float coefficientReductionDamage = DamageNormalizationFactor - Mathf.Exp(-targetArmor.ArmorValue / ArmorReductionScale);
        UnityEngine.Debug.Log("coefficientReductionArmor Value: " + coefficientReductionDamage);

        float damageAfterApplyingCoefficientReductionDamage = damage.Damage * (DamageNormalizationFactor - coefficientReductionDamage);
        UnityEngine.Debug.Log("damageAfterArmor Value: " + damageAfterApplyingCoefficientReductionDamage);

        float finalDamage = damageAfterApplyingCoefficientReductionDamage * coefficientDamage;
        UnityEngine.Debug.Log("finalDamage Value: " + finalDamage);

        if (finalDamage <= 0)
            return 0;

        return finalDamage;
    }
}
