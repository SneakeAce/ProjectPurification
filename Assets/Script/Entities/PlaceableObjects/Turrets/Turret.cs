using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Turret : MonoBehaviour, ITurret
{
    private TurretConfig _turretConfig;
    private AutomaticTurretAttack _turretAttack;
    private TurretHealth _turretHealth;
    private TurretSearchTargetSystem _searchTargetSystem;

    public TurretAttack TurretAttack => _turretAttack;

    public Transform Transform => transform;

    public Animator Animator => throw new System.NotImplementedException();

    public Rigidbody Rigidbody => throw new System.NotImplementedException();

    public Collider Collider => throw new System.NotImplementedException();

    [Inject]
    private void Construct(AutomaticTurretAttack turretAttack, TurretSearchTargetSystem searchTargetSystem)
    {
        _turretAttack = turretAttack;
        _searchTargetSystem = searchTargetSystem;


    }

    public void SetComponents(TurretConfig turretConfig)
    {
        _turretConfig = turretConfig;

        Initialize();
    }

    private void Initialize()
    {
        _searchTargetSystem.Start(this);

        _turretAttack.Initialize(_turretConfig);
        //_turretHealth;
    }

}