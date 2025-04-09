using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Bullet áóäåò ïîëó÷àòüñÿ èç ïóëà

    [SerializeField] private TurretConfig _turretConfig;
    [SerializeField] private WeaponConfig _weaponConfig;

    private Bullet _turretBullet;

    private TurretWeapon _weapon;

    public TurretType TurretType => _turretConfig.ÑharacteristicsTurret.TurretType;


}