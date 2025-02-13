using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretConfig _turretConfig;

    public TurretType TurretType => _turretConfig.SpecificationTurret.TurretType;
    public int MaxCountOnCurrentScene => _turretConfig.SpecificationTurret.MaxCountOnScene;
}
