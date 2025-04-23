using UnityEngine;
using Zenject;

public class Turret : MonoBehaviour, ITurret
{
    private TurretConfig _turretConfig;
    private TurretHealth _turretHealth;
    private TurretSearchTargetSystem _searchTargetSystem;
    private TurretWeapon _turretWeapon;

    protected IFactory<Bullet, BulletType> _bulletFactory;

    public Transform Transform => transform;

    public Animator Animator => throw new System.NotImplementedException();

    public Rigidbody Rigidbody => throw new System.NotImplementedException();

    public Collider Collider => throw new System.NotImplementedException();

    public TurretWeapon TurretAttack => _turretWeapon;

    [Inject]
    private void Construct(TurretSearchTargetSystem turretSearchTargetSystem, IFactory<Bullet, BulletType> bulletFactory)
    {
        _searchTargetSystem = turretSearchTargetSystem;
        _bulletFactory = bulletFactory;

        _turretWeapon = GetComponentInChildren<TurretWeapon>();
    }

    public void SetComponents(TurretConfig turretConfig)
    {
        _turretConfig = turretConfig;

        Initialize();
    }

    private void Initialize()
    {
        _turretWeapon.Initialize(this, _searchTargetSystem, _turretConfig, _bulletFactory);

        //_turretHealth;
    }

}