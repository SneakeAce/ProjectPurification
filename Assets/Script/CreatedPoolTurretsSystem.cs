using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolTurretsSystem
{
    private List<Turret> _turretObjects;

    private Dictionary<TurretType, ObjectPool<Turret>> _poolDictionary;

    public CreatedPoolTurretsSystem(IEnumerable<Turret> turretObjects)
    {
        _turretObjects = new List<Turret>(turretObjects);
    }

    public Dictionary<TurretType, ObjectPool<Turret>> PoolDictionary => _poolDictionary;

    public void Initialization()
    {
        _poolDictionary = new Dictionary<TurretType, ObjectPool<Turret>>();

        StartingCreatePools();
    }

    private void StartingCreatePools()
    {
        if (_turretObjects.Count > 0)
        {
            foreach (Turret turretObject in _turretObjects)
            {
                if (_poolDictionary.ContainsKey(turretObject.TurretType))
                    continue;

                switch (turretObject.TurretType)
                {
                    case TurretType.AutomaticTurret:
                        ObjectPool<Turret> poolAutomaticTurret = CreatePool(turretObject.TurretType, turretObject.MaxCountOnCurrentScene, turretObject);
                        _poolDictionary.Add(turretObject.TurretType, poolAutomaticTurret);
                        break;

                    case TurretType.MachineGunTurret:
                        ObjectPool<Turret> poolMachineGunTurret = CreatePool(turretObject.TurretType, turretObject.MaxCountOnCurrentScene, turretObject);
                        _poolDictionary.Add(turretObject.TurretType, poolMachineGunTurret);

                        break;

                    case TurretType.ArmorPiercingTurret:
                        ObjectPool<Turret> poolArmorPiercingTurret = CreatePool(turretObject.TurretType, turretObject.MaxCountOnCurrentScene, turretObject);
                        _poolDictionary.Add(turretObject.TurretType, poolArmorPiercingTurret);

                        break;

                    default:
                        throw new ArgumentException("This turret type does not exist");
                }
            }
        }
    }

    private ObjectPool<Turret> CreatePool(TurretType turretType, int maxPoolSize, Turret turretObject)
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
