using UnityEngine;
using Zenject;

public class WeaponFactory : IWeaponFactory
{
    private DiContainer _container;

    private Character _character;

    private ConfigsLibrariesHandler<WeaponConfig, WeaponType> _handlerWeaponConfigs;

    private InstantiateAndDestroyGameObjectPerformer _performerGameObject;

    public WeaponFactory(DiContainer container, ConfigsLibrariesHandler<WeaponConfig, WeaponType> handlerWeaponConfigs, 
        Character character, InstantiateAndDestroyGameObjectPerformer performerGameObject)
    {
        _container = container;
        _handlerWeaponConfigs = handlerWeaponConfigs;
        _character = character;
        _performerGameObject = performerGameObject;
    }

    public Weapon Create()
    {
        WeaponConfig config = GetConfig(WeaponType.RifleAK47); // Временно, потом переделаю

        GameObject instance = _performerGameObject.CreateObject(config.Prefab, config.Prefab.transform.position, config.Prefab.transform.rotation);

        Weapon weapon = instance.GetComponentInChildren<Weapon>();

        if (weapon == null)
        {
            Debug.Log("WeaponFactory / weapon == null (" + weapon + ")");
            return null;
        }

        SetParent(instance);

        _container.Inject(weapon);

        weapon.SetComponents(config, _character);

        return weapon;
    }

    private WeaponConfig GetConfig(WeaponType type)
    {
        WeaponConfig config = _handlerWeaponConfigs.GetObjectConfig(type);

        if (config == null)
            return null;

        return config;
    }

    private void SetParent(GameObject item)
    {
        item.transform.SetParent(_character.WeaponHolder.transform, false);
    }
}
