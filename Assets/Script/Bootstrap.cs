using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Weapon _weapon;


    private void Awake()
    {
        _weapon.Initialize();
    }
}
