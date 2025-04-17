using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class WeaponManager : MonoBehaviour
{
    private const int IndexOfDefaultWeapon = 0;

    private IWeaponFactory _factory;
    private WeaponSwitcher _switcher;

    private Character _character;
    private DiContainer _container;
    private Weapon _defaultWeapon;

    private List<Weapon> _weaponsList = new();
    private List<WeaponType> _weaponTypes = new();

    private Dictionary<WeaponType, WeaponConfig> _weaponConfigsDictionary = new();

    private ConfigsLibrariesHandler<WeaponConfig, WeaponType> _handlerWeaponConfigs;
                    
    [Inject]
    private void Construct(DiContainer container, Character character, IWeaponFactory factory, 
        WeaponSwitcher switcher, ConfigsLibrariesHandler<WeaponConfig, WeaponType> handlerWeaponConfigs)
    {
        _container = container;
        _character = character;

        _factory = factory;
        _switcher = switcher;
        _handlerWeaponConfigs = handlerWeaponConfigs;

        _factory.Initialize(_container, this, _character);

        Initialization();
    }

    public GameObject CreateObject(GameObject prefab)
    {
        GameObject instance = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);

        if (instance == null)
            return null;

        return instance;
    }

    public WeaponConfig GetConfig(WeaponType type)
    {
        WeaponConfig config = _handlerWeaponConfigs.GetObjectConfig(type);

        if (config == null)
            return null;

        return config;
    }

    private void Initialization()
    {
        _weaponTypes = GetAvailableWeaponTypes();

        _weaponConfigsDictionary = GetAvailableConfigs();

        CreateWeapon();

        SetBaseWeapon();

        _switcher.Initialize(_weaponsList, _defaultWeapon);
    }

    private void CreateWeapon()
    {
        foreach (var weaponConfig in _weaponConfigsDictionary.Values)
        {
            Weapon weapon = _factory.Create(weaponConfig);
            
            if (weapon != null)
            {
                _weaponsList.Add(weapon);
                weapon.gameObject.SetActive(false);
            }
        }
    }

    private List<WeaponType> GetAvailableWeaponTypes()
    {
        List<WeaponType> weaponTypes = Enum.GetValues(typeof(WeaponType))
            .Cast<WeaponType>()
            .ToList();

        if (weaponTypes.Count == 0)
            return new List<WeaponType>();

        return weaponTypes;
    }

    private Dictionary<WeaponType, WeaponConfig> GetAvailableConfigs()
    {
        Dictionary<WeaponType, WeaponConfig> availableWeaponConfigs = new();

        foreach (WeaponType type in _weaponTypes)
        {
            WeaponConfig config = _handlerWeaponConfigs.GetObjectConfig(type);

            if (config == null)
                continue;
            
           availableWeaponConfigs.Add(type, config);

        }

        return availableWeaponConfigs;
    }

    private void SetBaseWeapon()
    {
        _defaultWeapon = _weaponsList[IndexOfDefaultWeapon];
        _defaultWeapon.gameObject.SetActive(true);
    }

}
