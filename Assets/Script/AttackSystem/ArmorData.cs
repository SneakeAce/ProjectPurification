public readonly struct ArmorData
{
    private const float DefaultMinArmorValue = 0f;
    private const float DefaultMaxArmorValue = 100f;

    public ArmorData(ArmorType armorType, float armorValue)
    {
        ArmorType = armorType;
        ArmorValue = armorValue;
    }

    public ArmorType ArmorType { get; }
    public float ArmorValue { get; }
    public float MinArmorValue => DefaultMinArmorValue;
    public float MaxArmorValue => DefaultMaxArmorValue;
}
