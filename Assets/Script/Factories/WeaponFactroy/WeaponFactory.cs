using UnityEngine;
using Zenject;

public class WeaponFactory : IWeaponFactory
{
    private DiContainer _container;
    private Character _character;
    private WeaponManager _manager;

    public void Initialize(DiContainer container, WeaponManager manager, Character character)
    {
        _container = container;
        _character = character;
        _manager = manager;
    }

    public Weapon Create(WeaponConfig config)
    {
        GameObject instance = _manager.CreateObject(config.Prefab);

        Weapon weapon = instance.GetComponentInChildren<Weapon>();

        if (weapon == null)
            return null;

        SetParent(instance);

        BindAndInjectPerforming(weapon);

        weapon.SetComponents(config, _character);

        return weapon;
    }

    private void SetParent(GameObject item)
    {
        item.transform.SetParent(_character.WeaponHolder.transform, false);
    }

    private void BindAndInjectPerforming(Weapon weapon)
    {
        _container.Bind<Weapon>().FromInstance(weapon).AsSingle();
        _container.Inject(weapon);
    }
}
