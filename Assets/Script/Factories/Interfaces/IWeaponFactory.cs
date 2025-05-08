using Zenject;

public interface IWeaponFactory
{
    void Initialize(DiContainer container, WeaponManager manager, Character character);
    Weapon Create(WeaponConfig config);
}
