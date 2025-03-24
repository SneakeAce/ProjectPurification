using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolTurretsSystem : CreatedPoolSystem<Turret, TurretType>
{
    public CreatedPoolTurretsSystem(CreatedPoolTurretConfig config) : base(config)
    {
    }

    protected override void Initialization()
    {
        _poolDictionary = new Dictionary<TurretType, ObjectPool<Turret>>();

        StartingCreatePools();
    }

    protected override void StartingCreatePools()
    {
        if (_monoObjects.Count > 0)
        {
            foreach (Turret turretObject in _monoObjects)
            {
                if (_poolDictionary.ContainsKey(turretObject.TurretType))
                    continue;

                switch (turretObject.TurretType)
                {
                    case TurretType.AutomaticTurret:
                        ObjectPool<Turret> poolAutomaticTurret = CreatePool(turretObject.TurretType, turretObject, turretObject.MaxCountOnCurrentScene);
                        _poolDictionary.Add(turretObject.TurretType, poolAutomaticTurret);
                        break;

                    case TurretType.MachineGunTurret:
                        ObjectPool<Turret> poolMachineGunTurret = CreatePool(turretObject.TurretType, turretObject, turretObject.MaxCountOnCurrentScene);
                        _poolDictionary.Add(turretObject.TurretType, poolMachineGunTurret);

                        break;

                    case TurretType.ArmorPiercingTurret:
                        ObjectPool<Turret> poolArmorPiercingTurret = CreatePool(turretObject.TurretType, turretObject, turretObject.MaxCountOnCurrentScene);
                        _poolDictionary.Add(turretObject.TurretType, poolArmorPiercingTurret);

                        break;

                    default:
                        throw new ArgumentException("This turret type does not exist");
                }
            }
        }
    }

    protected override ObjectPool<Turret> CreatePool(TurretType turretType, Turret turretObject, int maxPoolSize)
    {
        ObjectPool<Turret> turretObjectPool;

        GameObject newHolder = new GameObject(turretType.ToString());
        newHolder.transform.SetParent(null);
        newHolder.transform.position = Vector3.zero;

        if (newHolder != null)
        {
            turretObjectPool = new ObjectPool<Turret>(turretObject, maxPoolSize, newHolder.transform);

            return turretObjectPool;
        }

        return null;
    }
}
