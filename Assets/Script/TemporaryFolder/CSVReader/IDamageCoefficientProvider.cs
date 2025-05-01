public interface IDamageCoefficientProvider
{
    float GetCoefficient(AttackType attackType, ArmorType armorType);
}
