using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SwitchWeaponHandler : MonoBehaviour
{
    private List<Weapon> _weaponsList = new();
    private Dictionary<WeaponType, Weapon> _weaponsDictionary = new();

    private Character _character;
    private IWeaponFactory _factory;

    [Inject]
    private void Construct(Character character, IWeaponFactory factory)
    {
        _character = character;
        _factory = factory;
    }

    private void Start()
    {
        _weaponsList.Add(_factory.Create());


    }

    //private void GetAllWeapons()
    //{
    //    _weaponsList = _character.WeaponHolder.GetComponentsInChildren<Weapon>().ToList();
    //}



}
