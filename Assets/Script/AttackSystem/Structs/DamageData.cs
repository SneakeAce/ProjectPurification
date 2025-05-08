public readonly struct DamageData
{
    public DamageData(AttackType attackType, float damage)
    {
        AttackType = attackType;
        Damage = damage;
    }

    public AttackType AttackType { get; }
    public float Damage { get; }
}
